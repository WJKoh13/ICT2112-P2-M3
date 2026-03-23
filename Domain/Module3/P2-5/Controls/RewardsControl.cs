using ProRental.Data.UnitOfWork;
using ProRental.Domain.Entities;
using ProRental.Interfaces.Data;
using ProRental.Interfaces.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace ProRental.Domain.Controls;

public class RewardsControl : IRewardsControl
{
    // Carbon thresholds (grams CO₂e)
    private const double LowThreshold      = 50.0;
    private const double ModerateThreshold = 100.0;
    private const double HighThreshold     = 200.0;

    private readonly IOrderCarbonDataGateway  _carbonGateway;
    private readonly IRewardGateway           _rewardGateway;
    private readonly IOrderGateway            _orderGateway;
    private readonly IProductFootprintService _productFootprintService;
    private readonly AppDbContext             _db;

    public RewardsControl(
        IOrderCarbonDataGateway  carbonGateway,
        IRewardGateway           rewardGateway,
        IOrderGateway            orderGateway,
        IProductFootprintService productFootprintService,
        AppDbContext             db)
    {
        _carbonGateway           = carbonGateway;
        _rewardGateway           = rewardGateway;
        _orderGateway            = orderGateway;
        _productFootprintService = productFootprintService;
        _db                      = db;
    }

    /// Aggregates carbon from all four sources, classifies impact level,
    /// and persists the result. Idempotent — returns existing record if
    /// already calculated for this order.
    public Ordercarbondatum CreateOrderCarbonData(int orderId, double totalCarbon)
    {
        Console.WriteLine(_db.Database.GetDbConnection().ConnectionString); 

        var existing = _carbonGateway.FindByOrderId(orderId);
        if (existing is not null) return existing;

        // Product carbon — from Feature 1 via IProductFootprintService
        double productCarbon = _productFootprintService.CalculateProductFootprint(orderId);

        // Packaging carbon — from Feature 3 (stub until integrated)
        double packagingCarbon = 0.0;

        // Staff carbon — sum all totalstaffco2 divided equally across all orders
        // Staff carbon represents the shared operational cost per order
        int    totalOrders     = _db.Orders.Count();
        double totalStaffCo2   = _db.Stafffootprints.Any()
            ? _db.Stafffootprints.Sum(s => s.Totalstaffco2)
            : 0.0;
        double staffCarbon = totalOrders > 0 ? totalStaffCo2 / totalOrders : 0.0;

        // Building carbon — sum all totalroomco2 divided equally across all orders
        // Building carbon represents the shared facility cost per order
        double totalBuildingCo2 = _db.Buildingfootprints.Any()
            ? _db.Buildingfootprints.Sum(b => b.Totalroomco2)
            : 0.0;
        double buildingCarbon = totalOrders > 0 ? totalBuildingCo2 / totalOrders : 0.0;

        double total = productCarbon + packagingCarbon + staffCarbon + buildingCarbon;

        var record = new Ordercarbondatum
        {
            Orderid         = orderId,
            Productcarbon   = productCarbon,
            Packagingcarbon = packagingCarbon,
            Staffcarbon     = staffCarbon,
            Buildingcarbon  = buildingCarbon,
            Totalcarbon     = total,
            Impactlevel     = ClassifyImpact(total),
            Calculatedat    = DateTime.UtcNow
        };

        _carbonGateway.Save(record);
        return record;
    }

    /// Calculates an eco-score (0–100) based on total carbon.
    /// Returns -1 if no carbon data exists for the order.
    public int CalculateEcoScore(int orderId)
    {
        var data = _carbonGateway.FindByOrderId(orderId);
        if (data is null) return -1;

        const double maxCarbon = 400.0;
        int score = (int)Math.Round(Math.Max(0, (1.0 - data.Totalcarbon / maxCarbon) * 100));
        return Math.Clamp(score, 0, 100);
    }

    /// Determines and persists a reward based on the order's impact level.
    /// Returns null if the order does not qualify.
    public Customerreward? DetermineReward(int orderId)
    {
        var data = _carbonGateway.FindByOrderId(orderId);
        if (data is null) return null;

        var existing = _rewardGateway.FindByOrderCarbonDataId(data.Ordercarbondataid);
        if (existing is not null) return existing;

        (string type, double value)? rewardInfo = data.Impactlevel switch
        {
            "Low"      => ("Voucher", 10.0),
            "Moderate" => ("Voucher", 5.0),
            _          => null
        };

        if (rewardInfo is null) return null;

        var reward = new Customerreward
        {
            Customerid        = 1, // placeholder until auth is integrated
            Ordercarbondataid = data.Ordercarbondataid,
            Rewardtype        = rewardInfo.Value.type,
            Rewardvalue       = rewardInfo.Value.value,
            Createdat         = DateTime.UtcNow
        };

        _rewardGateway.Save(reward);
        return reward;
    }

    public List<Order> GetAllOrders()
        => _orderGateway.FindAll();

    public List<Ordercarbondatum> GetAllCarbonRecords()
        => _carbonGateway.FindAll();

    public List<Customerreward> GetAllRewards()
        => _rewardGateway.FindAll();

    private static string ClassifyImpact(double totalCarbon) => totalCarbon switch
    {
        < LowThreshold      => "Low",
        < ModerateThreshold => "Moderate",
        < HighThreshold     => "High",
        _                   => "Very High"
    };
}