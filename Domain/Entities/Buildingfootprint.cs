using System;

namespace ProRental.Domain.Entities;

public partial class Buildingfootprint
{
    private int _buildingcarbonfootprintid;
    private DateTime _timehourly;
    private string? _zone;
    private string? _block;
    private string? _floor;
    private string? _room;
    private double _totalroomco2;

    public int GetBuildingcarbonfootprintid()
    {
        return _buildingcarbonfootprintid;
    }

    private void SetBuildingcarbonfootprintid(int buildingcarbonfootprintid)
    {
        _buildingcarbonfootprintid = buildingcarbonfootprintid;
    }

    public DateTime GetTimehourly()
    {
        return _timehourly;
    }

    private void SetTimehourly(DateTime timehourly)
    {
        _timehourly = timehourly;
    }

    public string? GetZone()
    {
        return _zone;
    }

    private void SetZone(string? zone)
    {
        _zone = zone;
    }

    public string? GetBlock()
    {
        return _block;
    }

    private void SetBlock(string? block)
    {
        _block = block;
    }

    public string? GetFloor()
    {
        return _floor;
    }

    private void SetFloor(string? floor)
    {
        _floor = floor;
    }

    public string? GetRoom()
    {
        return _room;
    }

    private void SetRoom(string? room)
    {
        _room = room;
    }

    public double GetTotalroomco2()
    {
        return _totalroomco2;
    }

    private void SetTotalroomco2(double totalroomco2)
    {
        _totalroomco2 = totalroomco2;
    }
}
