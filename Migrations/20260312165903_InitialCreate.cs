using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProRental.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:access_event_type", "IN,OUT")
                .Annotation("Npgsql:Enum:batch_status", "PENDING,IN_PROGRESS,COMPLETED,CANCELLED")
                .Annotation("Npgsql:Enum:carbon_stage_type", "DAMAGE_INSPECTION,REPAIRING,SERVICING,CLEANING,RETURN")
                .Annotation("Npgsql:Enum:cart_status_enum", "ACTIVE,CHECKED_OUT,EXPIRED")
                .Annotation("Npgsql:Enum:checkout_status_enum", "IN_PROGRESS,CONFIRMED,CANCELLED")
                .Annotation("Npgsql:Enum:clearance_batch_status", "SCHEDULED,ACTIVE,CLOSED")
                .Annotation("Npgsql:Enum:clearance_status", "CLEARANCE,SOLD")
                .Annotation("Npgsql:Enum:clearance_status_enum", "ONGOING,COMPLETED,CANCELLED")
                .Annotation("Npgsql:Enum:delivery_duration_enum", "NextDay,ThreeDays,OneWeek")
                .Annotation("Npgsql:Enum:delivery_type_enum", "STANDARD,EXPRESS,SELF_PICKUP")
                .Annotation("Npgsql:Enum:file_format_enum", "CSV,XLSX,PDF,PNG")
                .Annotation("Npgsql:Enum:hub_type", "WAREHOUSE,SHIPPING_PORT,AIRPORT")
                .Annotation("Npgsql:Enum:inventory_status", "AVAILABLE,RETIRED,CLEARANCE,SOLD,MAINTENANCE,RESERVED,ON_LOAN,BROKEN")
                .Annotation("Npgsql:Enum:loan_status", "OPEN,ON_LOAN,RETURNED")
                .Annotation("Npgsql:Enum:loan_status_enum", "ONGOING,RETURNED,OVERDUE,CANCELLED")
                .Annotation("Npgsql:Enum:log_type_enum", "RENTAL_ORDER,LOAN,RETURN,PURCHASE_ORDER,CLEARANCE")
                .Annotation("Npgsql:Enum:notification_frequency_enum", "INSTANT,DAILY,WEEKLY")
                .Annotation("Npgsql:Enum:notification_granularity_enum", "ALL,IMPORTANT_ONLY,NONE")
                .Annotation("Npgsql:Enum:notification_type_enum", "ORDER_UPDATE,PROMOTION,SYSTEM,PRODUCT")
                .Annotation("Npgsql:Enum:order_history_status_enum", "PENDING,CONFIRMED,PROCESSING,READY_FOR_DISPATCH,DISPATCHED,DELIVERED,CANCELLED")
                .Annotation("Npgsql:Enum:order_status_enum", "PENDING,CONFIRMED,PROCESSING,READY_FOR_DISPATCH,DISPATCHED,DELIVERED,CANCELLED")
                .Annotation("Npgsql:Enum:payment_method_enum", "CREDIT_CARD")
                .Annotation("Npgsql:Enum:payment_purpose_enum", "RENTAL_FEE_DEPOSIT,PENALTY_FEE")
                .Annotation("Npgsql:Enum:po_status_enum", "COMPLETED,CONFIRMED,SUBMITTED,APPROVED,REJECTED,CANCELLED")
                .Annotation("Npgsql:Enum:preference_type", "SPEED,COST,GREEN")
                .Annotation("Npgsql:Enum:product_status", "AVAILABLE,UNAVAILABLE,RETIRED")
                .Annotation("Npgsql:Enum:purchase_order_status_enum", "PENDING,APPROVED,REJECTED,DELIVERED,CANCELLED")
                .Annotation("Npgsql:Enum:rating_band_enum", "HIGH,MEDIUM,LOW,UNRATED")
                .Annotation("Npgsql:Enum:reason_code_enum", "LOWSTOCK,DEMANDSPIKE,REPLACEMENT,NEWITEM,OTHERS")
                .Annotation("Npgsql:Enum:rental_status_enum", "PENDING,CONFIRMED,CANCELLED,COMPLETED")
                .Annotation("Npgsql:Enum:replenishment_status_enum", "DRAFT,SUBMITTED,CANCELLED,COMPLETED")
                .Annotation("Npgsql:Enum:return_item_status", "DAMAGE_INSPECTION,REPAIRING,SERVICING,CLEANING,RETURN_TO_INVENTORY")
                .Annotation("Npgsql:Enum:return_request_status", "PROCESSING,COMPLETED")
                .Annotation("Npgsql:Enum:return_status_enum", "PENDING,APPROVED,REJECTED,COMPLETED")
                .Annotation("Npgsql:Enum:shipment_status_enum", "PENDING,IN_TRANSIT,DELIVERED,CANCELLED")
                .Annotation("Npgsql:Enum:supplier_category_enum", "LONGCREDITPERIOD,QUICKTURNAROUNDTIME,NEWUNTESTED")
                .Annotation("Npgsql:Enum:transaction_purpose_enum", "ORDER,PENALTY,REFUND_DEPOSIT")
                .Annotation("Npgsql:Enum:transaction_status_enum", "PENDING,COMPLETED,FAILED,CANCELLED")
                .Annotation("Npgsql:Enum:transaction_type_enum", "PAYMENT,REFUND")
                .Annotation("Npgsql:Enum:transport_mode", "TRUCK,SHIP,PLANE,TRAIN")
                .Annotation("Npgsql:Enum:transport_mode_combination", "TRUCK_ONLY,SHIP_TRUCK,AIR_TRUCK,RAIL_TRUCK,MULTIMODAL")
                .Annotation("Npgsql:Enum:vetting_decision_enum", "APPROVED,REJECTED,PENDING")
                .Annotation("Npgsql:Enum:vetting_result_enum", "APPROVED,REJECTED,PENDING")
                .Annotation("Npgsql:Enum:visual_type_enum", "TABLE,BAR,COLUMN,LINE,PIE,AREA");

            migrationBuilder.CreateTable(
                name: "analytics",
                columns: table => new
                {
                    analyticsid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    startdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    enddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    loanamt = table.Column<int>(type: "integer", nullable: true),
                    returnamt = table.Column<int>(type: "integer", nullable: true),
                    primarysupplier = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    primaryitem = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    supplierreliability = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    turnoverrate = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("analytics_pkey", x => x.analyticsid);
                });

            migrationBuilder.CreateTable(
                name: "buildingfootprint",
                columns: table => new
                {
                    buildingcarbonfootprintid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    timehourly = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    zone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    block = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    floor = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    room = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    totalroomco2 = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("buildingfootprint_pkey", x => x.buildingcarbonfootprintid);
                });

            migrationBuilder.CreateTable(
                name: "carbon_result",
                columns: table => new
                {
                    carbon_result_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    total_carbon_kg = table.Column<double>(type: "double precision", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()"),
                    validation_passed = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("carbon_result_pkey", x => x.carbon_result_id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    categoryid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    createddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updateddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("category_pkey", x => x.categoryid);
                });

            migrationBuilder.CreateTable(
                name: "clearancebatch",
                columns: table => new
                {
                    clearancebatchid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    batchname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    clearancedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clearancebatch_pkey", x => x.clearancebatchid);
                });

            migrationBuilder.CreateTable(
                name: "ecobadge",
                columns: table => new
                {
                    badgeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    maxcarbong = table.Column<double>(type: "double precision", nullable: false),
                    criteriadescription = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    badgename = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ecobadge_pkey", x => x.badgeid);
                });

            migrationBuilder.CreateTable(
                name: "packagingmaterial",
                columns: table => new
                {
                    materialid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    recyclable = table.Column<bool>(type: "boolean", nullable: false),
                    reusable = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("packagingmaterial_pkey", x => x.materialid);
                });

            migrationBuilder.CreateTable(
                name: "pricing_rule",
                columns: table => new
                {
                    rule_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    base_rate_per_km = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    carbon_surcharge = table.Column<decimal>(type: "numeric(10,4)", precision: 10, scale: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pricing_rule_pkey", x => x.rule_id);
                });

            migrationBuilder.CreateTable(
                name: "product_return",
                columns: table => new
                {
                    return_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    return_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    total_carbon = table.Column<double>(type: "double precision", nullable: true),
                    date_in = table.Column<DateOnly>(type: "date", nullable: true),
                    date_on = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_return_pkey", x => x.return_id);
                });

            migrationBuilder.CreateTable(
                name: "purchaseorder",
                columns: table => new
                {
                    poid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    supplierid = table.Column<int>(type: "integer", nullable: true),
                    podate = table.Column<DateOnly>(type: "date", nullable: true),
                    expecteddeliverydate = table.Column<DateOnly>(type: "date", nullable: true),
                    totalamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("purchaseorder_pkey", x => x.poid);
                });

            migrationBuilder.CreateTable(
                name: "replenishmentrequest",
                columns: table => new
                {
                    requestid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    requestedby = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    completedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    completedby = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("replenishmentrequest_pkey", x => x.requestid);
                });

            migrationBuilder.CreateTable(
                name: "stockitem",
                columns: table => new
                {
                    productid = table.Column<int>(type: "integer", nullable: false),
                    sku = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    uom = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("stockitem_pkey", x => x.productid);
                });

            migrationBuilder.CreateTable(
                name: "supplier",
                columns: table => new
                {
                    supplierid = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    details = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    creditperiod = table.Column<int>(type: "integer", nullable: true),
                    avgturnaroundtime = table.Column<double>(type: "double precision", nullable: true),
                    isverified = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("supplier_pkey", x => x.supplierid);
                });

            migrationBuilder.CreateTable(
                name: "transactionlog",
                columns: table => new
                {
                    transactionlogid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("transactionlog_pkey", x => x.transactionlogid);
                });

            migrationBuilder.CreateTable(
                name: "transport",
                columns: table => new
                {
                    transport_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    max_load_kg = table.Column<double>(type: "double precision", nullable: true),
                    vehicle_size_m2 = table.Column<double>(type: "double precision", nullable: true),
                    is_available = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transport_pkey", x => x.transport_id);
                });

            migrationBuilder.CreateTable(
                name: "transportation_hub",
                columns: table => new
                {
                    hub_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    longitude = table.Column<double>(type: "double precision", nullable: false),
                    latitude = table.Column<double>(type: "double precision", nullable: false),
                    country_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    operational_status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    operation_time = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transportation_hub_pkey", x => x.hub_id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    passwordhash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    phonecountry = table.Column<int>(type: "integer", nullable: true),
                    phonenumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("User_pkey", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "reportexport",
                columns: table => new
                {
                    reportid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    refanalyticsid = table.Column<int>(type: "integer", nullable: true),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("reportexport_pkey", x => x.reportid);
                    table.ForeignKey(
                        name: "fk_reportexport_analytics",
                        column: x => x.refanalyticsid,
                        principalTable: "analytics",
                        principalColumn: "analyticsid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    productid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    categoryid = table.Column<int>(type: "integer", nullable: false),
                    sku = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("product_pkey", x => x.productid);
                    table.ForeignKey(
                        name: "fk_product_category",
                        column: x => x.categoryid,
                        principalTable: "category",
                        principalColumn: "categoryid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "return_stage",
                columns: table => new
                {
                    stage_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    return_id = table.Column<int>(type: "integer", nullable: false),
                    energy_kwh = table.Column<double>(type: "double precision", nullable: true),
                    labour_hours = table.Column<double>(type: "double precision", nullable: true),
                    materials_kg = table.Column<double>(type: "double precision", nullable: true),
                    cleaning_supplies_qty = table.Column<double>(type: "double precision", nullable: true),
                    water_litres = table.Column<double>(type: "double precision", nullable: true),
                    packaging_kg = table.Column<double>(type: "double precision", nullable: true),
                    storage_hours = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("return_stage_pkey", x => x.stage_id);
                    table.ForeignKey(
                        name: "fk_return_stage_return",
                        column: x => x.return_id,
                        principalTable: "product_return",
                        principalColumn: "return_id");
                });

            migrationBuilder.CreateTable(
                name: "polineitem",
                columns: table => new
                {
                    polineid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    poid = table.Column<int>(type: "integer", nullable: true),
                    productid = table.Column<int>(type: "integer", nullable: true),
                    qty = table.Column<int>(type: "integer", nullable: true),
                    unitprice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    linetotal = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("polineitem_pkey", x => x.polineid);
                    table.ForeignKey(
                        name: "fk_polineitem_po",
                        column: x => x.poid,
                        principalTable: "purchaseorder",
                        principalColumn: "poid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_product_stock",
                        column: x => x.productid,
                        principalTable: "stockitem",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reliabilityrating",
                columns: table => new
                {
                    ratingid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    supplierid = table.Column<int>(type: "integer", nullable: true),
                    score = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    rationale = table.Column<string>(type: "text", nullable: true),
                    calculatedbyuserid = table.Column<int>(type: "integer", nullable: true),
                    calculatedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("reliabilityrating_pkey", x => x.ratingid);
                    table.ForeignKey(
                        name: "fk_reliabilityrating_supplier",
                        column: x => x.supplierid,
                        principalTable: "supplier",
                        principalColumn: "supplierid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "suppliercategorychangelog",
                columns: table => new
                {
                    logid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    supplierid = table.Column<int>(type: "integer", nullable: true),
                    changereason = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    changedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("suppliercategorychangelog_pkey", x => x.logid);
                    table.ForeignKey(
                        name: "fk_suppliercatelog_supplier",
                        column: x => x.supplierid,
                        principalTable: "supplier",
                        principalColumn: "supplierid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "analyticslist",
                columns: table => new
                {
                    analyticsid = table.Column<int>(type: "integer", nullable: false),
                    transactionlogid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("analyticslist_pkey", x => new { x.analyticsid, x.transactionlogid });
                    table.ForeignKey(
                        name: "fk_analyticslist_analytics",
                        column: x => x.analyticsid,
                        principalTable: "analytics",
                        principalColumn: "analyticsid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_analyticslist_log",
                        column: x => x.transactionlogid,
                        principalTable: "transactionlog",
                        principalColumn: "transactionlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clearancelog",
                columns: table => new
                {
                    clearancelogid = table.Column<int>(type: "integer", nullable: false),
                    clearancebatchid = table.Column<int>(type: "integer", nullable: false),
                    batchname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    clearancedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    detailsjson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clearancelog_pkey", x => x.clearancelogid);
                    table.ForeignKey(
                        name: "fk_clearance_batch",
                        column: x => x.clearancebatchid,
                        principalTable: "clearancebatch",
                        principalColumn: "clearancebatchid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clearance_transaction",
                        column: x => x.clearancelogid,
                        principalTable: "transactionlog",
                        principalColumn: "transactionlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "purchaseorderlog",
                columns: table => new
                {
                    purchaseorderlogid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    poid = table.Column<int>(type: "integer", nullable: false),
                    podate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    supplierid = table.Column<int>(type: "integer", nullable: true),
                    expecteddeliverydate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    totalamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    detailsjson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("purchaseorderlog_pkey", x => x.purchaseorderlogid);
                    table.ForeignKey(
                        name: "fk_po_log_po",
                        column: x => x.poid,
                        principalTable: "purchaseorder",
                        principalColumn: "poid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_po_transaction",
                        column: x => x.purchaseorderlogid,
                        principalTable: "transactionlog",
                        principalColumn: "transactionlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plane",
                columns: table => new
                {
                    transport_id = table.Column<int>(type: "integer", nullable: false),
                    plane_id = table.Column<int>(type: "integer", nullable: false),
                    plane_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    plane_callsign = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("plane_pkey", x => x.transport_id);
                    table.ForeignKey(
                        name: "fk_plane_transport",
                        column: x => x.transport_id,
                        principalTable: "transport",
                        principalColumn: "transport_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ship",
                columns: table => new
                {
                    transport_id = table.Column<int>(type: "integer", nullable: false),
                    ship_id = table.Column<int>(type: "integer", nullable: false),
                    vessel_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    vessel_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    max_vessel_size = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ship_pkey", x => x.transport_id);
                    table.ForeignKey(
                        name: "fk_ship_transport",
                        column: x => x.transport_id,
                        principalTable: "transport",
                        principalColumn: "transport_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "train",
                columns: table => new
                {
                    transport_id = table.Column<int>(type: "integer", nullable: false),
                    train_id = table.Column<int>(type: "integer", nullable: false),
                    train_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    license_plate = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("train_pkey", x => x.transport_id);
                    table.ForeignKey(
                        name: "fk_train_transport",
                        column: x => x.transport_id,
                        principalTable: "transport",
                        principalColumn: "transport_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "truck",
                columns: table => new
                {
                    transport_id = table.Column<int>(type: "integer", nullable: false),
                    truck_id = table.Column<int>(type: "integer", nullable: false),
                    truck_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    license_plate = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("truck_pkey", x => x.transport_id);
                    table.ForeignKey(
                        name: "fk_truck_transport",
                        column: x => x.transport_id,
                        principalTable: "transport",
                        principalColumn: "transport_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "airport",
                columns: table => new
                {
                    hub_id = table.Column<int>(type: "integer", nullable: false),
                    airport_code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    airport_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    terminal = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("airport_pkey", x => x.hub_id);
                    table.ForeignKey(
                        name: "fk_airport_hub",
                        column: x => x.hub_id,
                        principalTable: "transportation_hub",
                        principalColumn: "hub_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "delivery_batch",
                columns: table => new
                {
                    delivery_batch_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    destination_address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    total_orders = table.Column<int>(type: "integer", nullable: true, defaultValue: 0),
                    carbon_savings = table.Column<double>(type: "double precision", nullable: true),
                    source_hub_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("delivery_batch_pkey", x => x.delivery_batch_id);
                    table.ForeignKey(
                        name: "fk_delivery_batch_hub",
                        column: x => x.source_hub_id,
                        principalTable: "transportation_hub",
                        principalColumn: "hub_id");
                });

            migrationBuilder.CreateTable(
                name: "route",
                columns: table => new
                {
                    route_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    origin_address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    destination_address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    total_distance_km = table.Column<double>(type: "double precision", nullable: true),
                    is_valid = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true),
                    origin_hub_id = table.Column<int>(type: "integer", nullable: true),
                    destination_hub_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("route_pkey", x => x.route_id);
                    table.ForeignKey(
                        name: "fk_route_destination_hub",
                        column: x => x.destination_hub_id,
                        principalTable: "transportation_hub",
                        principalColumn: "hub_id");
                    table.ForeignKey(
                        name: "fk_route_origin_hub",
                        column: x => x.origin_hub_id,
                        principalTable: "transportation_hub",
                        principalColumn: "hub_id");
                });

            migrationBuilder.CreateTable(
                name: "shipping_port",
                columns: table => new
                {
                    hub_id = table.Column<int>(type: "integer", nullable: false),
                    port_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    port_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    port_type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    vessel_size = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shipping_port_pkey", x => x.hub_id);
                    table.ForeignKey(
                        name: "fk_shipping_port_hub",
                        column: x => x.hub_id,
                        principalTable: "transportation_hub",
                        principalColumn: "hub_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "warehouse",
                columns: table => new
                {
                    hub_id = table.Column<int>(type: "integer", nullable: false),
                    warehouse_code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    total_warehouse_volume = table.Column<double>(type: "double precision", nullable: true),
                    climate_control_emission_rate = table.Column<double>(type: "double precision", nullable: true),
                    lighting_emission_rate = table.Column<double>(type: "double precision", nullable: true),
                    security_system_emission_rate = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("warehouse_pkey", x => x.hub_id);
                    table.ForeignKey(
                        name: "fk_warehouse_hub",
                        column: x => x.hub_id,
                        principalTable: "transportation_hub",
                        principalColumn: "hub_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customerid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    customertype = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customer_pkey", x => x.customerid);
                    table.ForeignKey(
                        name: "fk_customer_user",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notification",
                columns: table => new
                {
                    notificationid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    message = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    datesent = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    isread = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("notification_pkey", x => x.notificationid);
                    table.ForeignKey(
                        name: "fk_notification_user",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notificationpreference",
                columns: table => new
                {
                    preferenceid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    emailenabled = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    smsenabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("notificationpreference_pkey", x => x.preferenceid);
                    table.ForeignKey(
                        name: "fk_notificationpref_user",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "session",
                columns: table => new
                {
                    sessionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    expiresat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("session_pkey", x => x.sessionid);
                    table.ForeignKey(
                        name: "fk_session_user",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staff",
                columns: table => new
                {
                    staffid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    userid = table.Column<int>(type: "integer", nullable: false),
                    department = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("staff_pkey", x => x.staffid);
                    table.ForeignKey(
                        name: "fk_staff_user",
                        column: x => x.userid,
                        principalTable: "User",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "inventoryitem",
                columns: table => new
                {
                    inventoryid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    serialnumber = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    expirydate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("inventoryitem_pkey", x => x.inventoryid);
                    table.ForeignKey(
                        name: "fk_inventory_product",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lineitem",
                columns: table => new
                {
                    lineitemid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    requestid = table.Column<int>(type: "integer", nullable: true),
                    productid = table.Column<int>(type: "integer", nullable: true),
                    quantityrequest = table.Column<int>(type: "integer", nullable: true),
                    remarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("lineitem_pkey", x => x.lineitemid);
                    table.ForeignKey(
                        name: "fk_lineitem_product",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_lineitem_request",
                        column: x => x.requestid,
                        principalTable: "replenishmentrequest",
                        principalColumn: "requestid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productdetails",
                columns: table => new
                {
                    detailsid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    totalquantity = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    weight = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    image = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    depositrate = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("productdetails_pkey", x => x.detailsid);
                    table.ForeignKey(
                        name: "fk_productdetails_product",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productfootprint",
                columns: table => new
                {
                    productcarbonfootprintid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    badgeid = table.Column<int>(type: "integer", nullable: false),
                    producttoxicpercentage = table.Column<double>(type: "double precision", nullable: true),
                    totalco2 = table.Column<double>(type: "double precision", nullable: false),
                    calculatedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("productfootprint_pkey", x => x.productcarbonfootprintid);
                    table.ForeignKey(
                        name: "fk_productfootprint_badge",
                        column: x => x.badgeid,
                        principalTable: "ecobadge",
                        principalColumn: "badgeid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_productfootprint_product",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "carbon_emission",
                columns: table => new
                {
                    emission_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    stage_id = table.Column<int>(type: "integer", nullable: false),
                    carbon_kg = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("carbon_emission_pkey", x => x.emission_id);
                    table.ForeignKey(
                        name: "fk_carbon_emission_stage",
                        column: x => x.stage_id,
                        principalTable: "return_stage",
                        principalColumn: "stage_id");
                });

            migrationBuilder.CreateTable(
                name: "vettingrecord",
                columns: table => new
                {
                    vettingid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ratingid = table.Column<int>(type: "integer", nullable: true),
                    supplierid = table.Column<int>(type: "integer", nullable: true),
                    vettedbyuserid = table.Column<int>(type: "integer", nullable: true),
                    vettedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("vettingrecord_pkey", x => x.vettingid);
                    table.ForeignKey(
                        name: "fk_vettingrecord_rating",
                        column: x => x.ratingid,
                        principalTable: "reliabilityrating",
                        principalColumn: "ratingid",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_vettingrecord_supplier",
                        column: x => x.supplierid,
                        principalTable: "supplier",
                        principalColumn: "supplierid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "route_leg",
                columns: table => new
                {
                    leg_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    route_id = table.Column<int>(type: "integer", nullable: false),
                    sequence = table.Column<int>(type: "integer", nullable: true),
                    start_point = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    end_point = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    distance_km = table.Column<double>(type: "double precision", nullable: true),
                    is_first_mile = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    is_last_mile = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    transport_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("route_leg_pkey", x => x.leg_id);
                    table.ForeignKey(
                        name: "fk_route_leg_route",
                        column: x => x.route_id,
                        principalTable: "route",
                        principalColumn: "route_id");
                    table.ForeignKey(
                        name: "fk_route_leg_transport",
                        column: x => x.transport_id,
                        principalTable: "transport",
                        principalColumn: "transport_id");
                });

            migrationBuilder.CreateTable(
                name: "shipping_option",
                columns: table => new
                {
                    option_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    display_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    cost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    carbon_footprint = table.Column<double>(type: "double precision", nullable: true),
                    delivery_days = table.Column<int>(type: "integer", nullable: true),
                    is_green_option = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    route_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shipping_option_pkey", x => x.option_id);
                    table.ForeignKey(
                        name: "fk_shipping_option_route",
                        column: x => x.route_id,
                        principalTable: "route",
                        principalColumn: "route_id");
                });

            migrationBuilder.CreateTable(
                name: "customerrewards",
                columns: table => new
                {
                    customerrewardsid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    discount = table.Column<double>(type: "double precision", nullable: false),
                    totalcarbon = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("customerrewards_pkey", x => x.customerrewardsid);
                    table.ForeignKey(
                        name: "fk_customerrewards_customer",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    cartid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    customerid = table.Column<int>(type: "integer", nullable: true),
                    sessionid = table.Column<int>(type: "integer", nullable: true),
                    rentalstart = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    rentalend = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cart_pkey", x => x.cartid);
                    table.ForeignKey(
                        name: "fk_cart_customer",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_cart_session",
                        column: x => x.sessionid,
                        principalTable: "session",
                        principalColumn: "sessionid",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "staffaccesslog",
                columns: table => new
                {
                    accessid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    staffid = table.Column<int>(type: "integer", nullable: false),
                    eventtime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("staffaccesslog_pkey", x => x.accessid);
                    table.ForeignKey(
                        name: "fk_staffaccesslog_staff",
                        column: x => x.staffid,
                        principalTable: "staff",
                        principalColumn: "staffid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stafffootprint",
                columns: table => new
                {
                    staffcarbonfootprintid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    staffid = table.Column<int>(type: "integer", nullable: false),
                    time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    hoursworked = table.Column<double>(type: "double precision", nullable: false),
                    totalstaffco2 = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("stafffootprint_pkey", x => x.staffcarbonfootprintid);
                    table.ForeignKey(
                        name: "fk_stafffootprint_staff",
                        column: x => x.staffid,
                        principalTable: "staff",
                        principalColumn: "staffid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "clearanceitem",
                columns: table => new
                {
                    clearanceitemid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    clearancebatchid = table.Column<int>(type: "integer", nullable: false),
                    inventoryitemid = table.Column<int>(type: "integer", nullable: false),
                    finalprice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    recommendedprice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    saledate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("clearanceitem_pkey", x => x.clearanceitemid);
                    table.ForeignKey(
                        name: "fk_clearance_batch",
                        column: x => x.clearancebatchid,
                        principalTable: "clearancebatch",
                        principalColumn: "clearancebatchid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_clearance_inventory",
                        column: x => x.inventoryitemid,
                        principalTable: "inventoryitem",
                        principalColumn: "inventoryid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "leg_carbon",
                columns: table => new
                {
                    leg_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    distance_km = table.Column<double>(type: "double precision", nullable: true),
                    weight_kg = table.Column<double>(type: "double precision", nullable: true),
                    carbon_kg = table.Column<double>(type: "double precision", nullable: true),
                    carbon_rate = table.Column<double>(type: "double precision", nullable: true),
                    carbon_result_id = table.Column<int>(type: "integer", nullable: true),
                    route_leg_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("leg_carbon_pkey", x => x.leg_id);
                    table.ForeignKey(
                        name: "fk_leg_carbon_leg",
                        column: x => x.route_leg_id,
                        principalTable: "route_leg",
                        principalColumn: "leg_id");
                    table.ForeignKey(
                        name: "fk_leg_carbon_result",
                        column: x => x.carbon_result_id,
                        principalTable: "carbon_result",
                        principalColumn: "carbon_result_id");
                });

            migrationBuilder.CreateTable(
                name: "cartitem",
                columns: table => new
                {
                    cartitemid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    cartid = table.Column<int>(type: "integer", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    isselected = table.Column<bool>(type: "boolean", nullable: true, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cartitem_pkey", x => x.cartitemid);
                    table.ForeignKey(
                        name: "fk_cartitem_cart",
                        column: x => x.cartid,
                        principalTable: "cart",
                        principalColumn: "cartid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cartitem_product",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "checkout",
                columns: table => new
                {
                    checkoutid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    cartid = table.Column<int>(type: "integer", nullable: false),
                    deliverymethodid = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    notifyoptin = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("checkout_pkey", x => x.checkoutid);
                    table.ForeignKey(
                        name: "fk_checkout_cart",
                        column: x => x.cartid,
                        principalTable: "cart",
                        principalColumn: "cartid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_checkout_customer",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    orderid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    checkoutid = table.Column<int>(type: "integer", nullable: false),
                    orderdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    totalamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Order_pkey", x => x.orderid);
                    table.ForeignKey(
                        name: "fk_order_checkout",
                        column: x => x.checkoutid,
                        principalTable: "checkout",
                        principalColumn: "checkoutid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_customer",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "batch_order",
                columns: table => new
                {
                    batch_id = table.Column<int>(type: "integer", nullable: false),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    added_timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("batch_order_pkey", x => new { x.batch_id, x.order_id });
                    table.ForeignKey(
                        name: "fk_batch_order_batch",
                        column: x => x.batch_id,
                        principalTable: "delivery_batch",
                        principalColumn: "delivery_batch_id");
                    table.ForeignKey(
                        name: "fk_batch_order_order",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "customer_choice",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("customer_choice_pkey", x => new { x.customer_id, x.order_id });
                    table.ForeignKey(
                        name: "fk_customerchoice_customer",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_customerchoice_order",
                        column: x => x.order_id,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deliverymethod",
                columns: table => new
                {
                    deliveryid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    durationdays = table.Column<int>(type: "integer", nullable: false),
                    deliverycost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    carrierid = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("deliverymethod_pkey", x => x.deliveryid);
                    table.ForeignKey(
                        name: "fk_deliverymethod_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "loanlist",
                columns: table => new
                {
                    loanlistid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    loandate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    duedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    returndate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    remarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("loanlist_pkey", x => x.loanlistid);
                    table.ForeignKey(
                        name: "fk_loan_customer",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_loan_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orderitem",
                columns: table => new
                {
                    orderitemid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    productid = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    unitprice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    rentalstartdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    rentalenddate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orderitem_pkey", x => x.orderitemid);
                    table.ForeignKey(
                        name: "fk_orderitem_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_orderitem_product",
                        column: x => x.productid,
                        principalTable: "product",
                        principalColumn: "productid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orderstatushistory",
                columns: table => new
                {
                    historyid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    updatedby = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    remark = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("orderstatushistory_pkey", x => x.historyid);
                    table.ForeignKey(
                        name: "fk_order_status_history_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "packagingprofile",
                columns: table => new
                {
                    profileid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    volume = table.Column<double>(type: "double precision", nullable: false),
                    fragilitylevel = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("packagingprofile_pkey", x => x.profileid);
                    table.ForeignKey(
                        name: "fk_packagingprofile_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "refund",
                columns: table => new
                {
                    refundid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    depositrefundamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    returndate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    penaltyamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValue: 0.00m),
                    returnmethod = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("refund_pkey", x => x.refundid);
                    table.ForeignKey(
                        name: "fk_refund_customer",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_refund_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rentalorderlog",
                columns: table => new
                {
                    rentalorderlogid = table.Column<int>(type: "integer", nullable: false),
                    orderid = table.Column<int>(type: "integer", nullable: true),
                    customerid = table.Column<int>(type: "integer", nullable: true),
                    orderdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    totalamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    detailsjson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("rentalorderlog_pkey", x => x.rentalorderlogid);
                    table.ForeignKey(
                        name: "fk_rental_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rental_transaction",
                        column: x => x.rentalorderlogid,
                        principalTable: "transactionlog",
                        principalColumn: "transactionlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "returnrequest",
                columns: table => new
                {
                    returnrequestid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    customerid = table.Column<int>(type: "integer", nullable: false),
                    requestdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    completiondate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("returnrequest_pkey", x => x.returnrequestid);
                    table.ForeignKey(
                        name: "fk_returnrequest_customer",
                        column: x => x.customerid,
                        principalTable: "customer",
                        principalColumn: "customerid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_returnrequest_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "shipment",
                columns: table => new
                {
                    trackingid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    batchid = table.Column<int>(type: "integer", nullable: false),
                    weight = table.Column<double>(type: "double precision", nullable: false),
                    destination = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shipment_pkey", x => x.trackingid);
                    table.ForeignKey(
                        name: "fk_shipment_batch",
                        column: x => x.batchid,
                        principalTable: "delivery_batch",
                        principalColumn: "delivery_batch_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_shipment_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    transactionid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    providertransactionid = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transaction_pkey", x => x.transactionid);
                    table.ForeignKey(
                        name: "fk_transaction_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loanitem",
                columns: table => new
                {
                    loanitemid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    loanlistid = table.Column<int>(type: "integer", nullable: false),
                    inventoryitemid = table.Column<int>(type: "integer", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("loanitem_pkey", x => x.loanitemid);
                    table.ForeignKey(
                        name: "fk_loanitem_inventory",
                        column: x => x.inventoryitemid,
                        principalTable: "inventoryitem",
                        principalColumn: "inventoryid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_loanitem_loan",
                        column: x => x.loanlistid,
                        principalTable: "loanlist",
                        principalColumn: "loanlistid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "packagingconfiguration",
                columns: table => new
                {
                    configurationid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    profileid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("packagingconfiguration_pkey", x => x.configurationid);
                    table.ForeignKey(
                        name: "fk_packagingconfiguration_profile",
                        column: x => x.profileid,
                        principalTable: "packagingprofile",
                        principalColumn: "profileid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "loanlog",
                columns: table => new
                {
                    loanlogid = table.Column<int>(type: "integer", nullable: false),
                    loanlistid = table.Column<int>(type: "integer", nullable: false),
                    rentalorderlogid = table.Column<int>(type: "integer", nullable: false),
                    loandate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    returndate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    duedate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    detailsjson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("loanlog_pkey", x => x.loanlogid);
                    table.ForeignKey(
                        name: "fk_loan_list",
                        column: x => x.loanlistid,
                        principalTable: "loanlist",
                        principalColumn: "loanlistid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_loan_rental",
                        column: x => x.rentalorderlogid,
                        principalTable: "rentalorderlog",
                        principalColumn: "rentalorderlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_loan_transaction",
                        column: x => x.loanlogid,
                        principalTable: "transactionlog",
                        principalColumn: "transactionlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "returnitem",
                columns: table => new
                {
                    returnitemid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    returnrequestid = table.Column<int>(type: "integer", nullable: false),
                    inventoryitemid = table.Column<int>(type: "integer", nullable: false),
                    completiondate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    image = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("returnitem_pkey", x => x.returnitemid);
                    table.ForeignKey(
                        name: "fk_returnitem_inventory",
                        column: x => x.inventoryitemid,
                        principalTable: "inventoryitem",
                        principalColumn: "inventoryid",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_returnitem_request",
                        column: x => x.returnrequestid,
                        principalTable: "returnrequest",
                        principalColumn: "returnrequestid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "returnlog",
                columns: table => new
                {
                    returnlogid = table.Column<int>(type: "integer", nullable: false),
                    returnrequestid = table.Column<int>(type: "integer", nullable: false),
                    rentalorderlogid = table.Column<int>(type: "integer", nullable: false),
                    customerid = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    requestdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    completiondate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    imageurl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    detailsjson = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("returnlog_pkey", x => x.returnlogid);
                    table.ForeignKey(
                        name: "fk_return_rental",
                        column: x => x.rentalorderlogid,
                        principalTable: "rentalorderlog",
                        principalColumn: "rentalorderlogid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_return_request",
                        column: x => x.returnrequestid,
                        principalTable: "returnrequest",
                        principalColumn: "returnrequestid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_return_transaction",
                        column: x => x.returnlogid,
                        principalTable: "transactionlog",
                        principalColumn: "transactionlogid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deposit",
                columns: table => new
                {
                    depositid = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    transactionid = table.Column<int>(type: "integer", nullable: false),
                    originalamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    heldamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    refundedamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValue: 0m),
                    forfeitedamount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true, defaultValue: 0m),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("deposit_pkey", x => x.depositid);
                    table.ForeignKey(
                        name: "fk_deposit_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_deposit_transaction",
                        column: x => x.transactionid,
                        principalTable: "transaction",
                        principalColumn: "transactionid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "payment",
                columns: table => new
                {
                    paymentid = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    orderid = table.Column<int>(type: "integer", nullable: false),
                    transactionid = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("payment_pkey", x => x.paymentid);
                    table.ForeignKey(
                        name: "fk_payment_order",
                        column: x => x.orderid,
                        principalTable: "Order",
                        principalColumn: "orderid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_payment_transaction",
                        column: x => x.transactionid,
                        principalTable: "transaction",
                        principalColumn: "transactionid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "packagingconfigmaterials",
                columns: table => new
                {
                    configurationid = table.Column<int>(type: "integer", nullable: false),
                    materialid = table.Column<int>(type: "integer", nullable: false),
                    category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("packagingconfigmaterials_pkey", x => new { x.configurationid, x.materialid });
                    table.ForeignKey(
                        name: "fk_pcm_configuration",
                        column: x => x.configurationid,
                        principalTable: "packagingconfiguration",
                        principalColumn: "configurationid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_pcm_material",
                        column: x => x.materialid,
                        principalTable: "packagingmaterial",
                        principalColumn: "materialid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "damagereport",
                columns: table => new
                {
                    damagereportid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    returnitemid = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    severity = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    repaircost = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    images = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    reportdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("damagereport_pkey", x => x.damagereportid);
                    table.ForeignKey(
                        name: "fk_damagereport_returnitem",
                        column: x => x.returnitemid,
                        principalTable: "returnitem",
                        principalColumn: "returnitemid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_analyticslist_transactionlogid",
                table: "analyticslist",
                column: "transactionlogid");

            migrationBuilder.CreateIndex(
                name: "IX_batch_order_order_id",
                table: "batch_order",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_carbon_emission_stage_id",
                table: "carbon_emission",
                column: "stage_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_customerid",
                table: "cart",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_cart_sessionid",
                table: "cart",
                column: "sessionid");

            migrationBuilder.CreateIndex(
                name: "IX_cartitem_cartid",
                table: "cartitem",
                column: "cartid");

            migrationBuilder.CreateIndex(
                name: "IX_cartitem_productid",
                table: "cartitem",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_checkout_cartid",
                table: "checkout",
                column: "cartid");

            migrationBuilder.CreateIndex(
                name: "IX_checkout_customerid",
                table: "checkout",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "clearanceitem_inventoryitemid_key",
                table: "clearanceitem",
                column: "inventoryitemid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clearanceitem_clearancebatchid",
                table: "clearanceitem",
                column: "clearancebatchid");

            migrationBuilder.CreateIndex(
                name: "IX_clearancelog_clearancebatchid",
                table: "clearancelog",
                column: "clearancebatchid");

            migrationBuilder.CreateIndex(
                name: "IX_customer_userid",
                table: "customer",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_customer_choice_order_id",
                table: "customer_choice",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_customerrewards_customerid",
                table: "customerrewards",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_damagereport_returnitemid",
                table: "damagereport",
                column: "returnitemid");

            migrationBuilder.CreateIndex(
                name: "IX_delivery_batch_source_hub_id",
                table: "delivery_batch",
                column: "source_hub_id");

            migrationBuilder.CreateIndex(
                name: "IX_deliverymethod_orderid",
                table: "deliverymethod",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_deposit_orderid",
                table: "deposit",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_deposit_transactionid",
                table: "deposit",
                column: "transactionid");

            migrationBuilder.CreateIndex(
                name: "inventoryitem_serialnumber_key",
                table: "inventoryitem",
                column: "serialnumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventoryitem_productid",
                table: "inventoryitem",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_leg_carbon_carbon_result_id",
                table: "leg_carbon",
                column: "carbon_result_id");

            migrationBuilder.CreateIndex(
                name: "IX_leg_carbon_route_leg_id",
                table: "leg_carbon",
                column: "route_leg_id");

            migrationBuilder.CreateIndex(
                name: "IX_lineitem_productid",
                table: "lineitem",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_lineitem_requestid",
                table: "lineitem",
                column: "requestid");

            migrationBuilder.CreateIndex(
                name: "IX_loanitem_inventoryitemid",
                table: "loanitem",
                column: "inventoryitemid");

            migrationBuilder.CreateIndex(
                name: "IX_loanitem_loanlistid",
                table: "loanitem",
                column: "loanlistid");

            migrationBuilder.CreateIndex(
                name: "IX_loanlist_customerid",
                table: "loanlist",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_loanlist_orderid",
                table: "loanlist",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_loanlog_loanlistid",
                table: "loanlog",
                column: "loanlistid");

            migrationBuilder.CreateIndex(
                name: "IX_loanlog_rentalorderlogid",
                table: "loanlog",
                column: "rentalorderlogid");

            migrationBuilder.CreateIndex(
                name: "IX_notification_userid",
                table: "notification",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_notificationpreference_userid",
                table: "notificationpreference",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_checkoutid",
                table: "Order",
                column: "checkoutid");

            migrationBuilder.CreateIndex(
                name: "IX_Order_customerid",
                table: "Order",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_orderitem_orderid",
                table: "orderitem",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_orderitem_productid",
                table: "orderitem",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_orderstatushistory_orderid",
                table: "orderstatushistory",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_packagingconfigmaterials_materialid",
                table: "packagingconfigmaterials",
                column: "materialid");

            migrationBuilder.CreateIndex(
                name: "IX_packagingconfiguration_profileid",
                table: "packagingconfiguration",
                column: "profileid");

            migrationBuilder.CreateIndex(
                name: "IX_packagingprofile_orderid",
                table: "packagingprofile",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_payment_orderid",
                table: "payment",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_payment_transactionid",
                table: "payment",
                column: "transactionid");

            migrationBuilder.CreateIndex(
                name: "IX_polineitem_poid",
                table: "polineitem",
                column: "poid");

            migrationBuilder.CreateIndex(
                name: "IX_polineitem_productid",
                table: "polineitem",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_product_categoryid",
                table: "product",
                column: "categoryid");

            migrationBuilder.CreateIndex(
                name: "productdetails_productid_key",
                table: "productdetails",
                column: "productid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_productfootprint_badgeid",
                table: "productfootprint",
                column: "badgeid");

            migrationBuilder.CreateIndex(
                name: "IX_productfootprint_productid",
                table: "productfootprint",
                column: "productid");

            migrationBuilder.CreateIndex(
                name: "IX_purchaseorderlog_poid",
                table: "purchaseorderlog",
                column: "poid");

            migrationBuilder.CreateIndex(
                name: "IX_refund_customerid",
                table: "refund",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_refund_orderid",
                table: "refund",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_reliabilityrating_supplierid",
                table: "reliabilityrating",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_rentalorderlog_orderid",
                table: "rentalorderlog",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_reportexport_refanalyticsid",
                table: "reportexport",
                column: "refanalyticsid");

            migrationBuilder.CreateIndex(
                name: "IX_return_stage_return_id",
                table: "return_stage",
                column: "return_id");

            migrationBuilder.CreateIndex(
                name: "IX_returnitem_inventoryitemid",
                table: "returnitem",
                column: "inventoryitemid");

            migrationBuilder.CreateIndex(
                name: "IX_returnitem_returnrequestid",
                table: "returnitem",
                column: "returnrequestid");

            migrationBuilder.CreateIndex(
                name: "IX_returnlog_rentalorderlogid",
                table: "returnlog",
                column: "rentalorderlogid");

            migrationBuilder.CreateIndex(
                name: "IX_returnlog_returnrequestid",
                table: "returnlog",
                column: "returnrequestid");

            migrationBuilder.CreateIndex(
                name: "IX_returnrequest_customerid",
                table: "returnrequest",
                column: "customerid");

            migrationBuilder.CreateIndex(
                name: "IX_returnrequest_orderid",
                table: "returnrequest",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_route_destination_hub_id",
                table: "route",
                column: "destination_hub_id");

            migrationBuilder.CreateIndex(
                name: "IX_route_origin_hub_id",
                table: "route",
                column: "origin_hub_id");

            migrationBuilder.CreateIndex(
                name: "IX_route_leg_route_id",
                table: "route_leg",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "IX_route_leg_transport_id",
                table: "route_leg",
                column: "transport_id");

            migrationBuilder.CreateIndex(
                name: "IX_session_userid",
                table: "session",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_shipment_batchid",
                table: "shipment",
                column: "batchid");

            migrationBuilder.CreateIndex(
                name: "IX_shipment_orderid",
                table: "shipment",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_option_route_id",
                table: "shipping_option",
                column: "route_id");

            migrationBuilder.CreateIndex(
                name: "IX_staff_userid",
                table: "staff",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_staffaccesslog_staffid",
                table: "staffaccesslog",
                column: "staffid");

            migrationBuilder.CreateIndex(
                name: "IX_stafffootprint_staffid",
                table: "stafffootprint",
                column: "staffid");

            migrationBuilder.CreateIndex(
                name: "IX_suppliercategorychangelog_supplierid",
                table: "suppliercategorychangelog",
                column: "supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_orderid",
                table: "transaction",
                column: "orderid");

            migrationBuilder.CreateIndex(
                name: "User_email_key",
                table: "User",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_vettingrecord_ratingid",
                table: "vettingrecord",
                column: "ratingid");

            migrationBuilder.CreateIndex(
                name: "IX_vettingrecord_supplierid",
                table: "vettingrecord",
                column: "supplierid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "airport");

            migrationBuilder.DropTable(
                name: "analyticslist");

            migrationBuilder.DropTable(
                name: "batch_order");

            migrationBuilder.DropTable(
                name: "buildingfootprint");

            migrationBuilder.DropTable(
                name: "carbon_emission");

            migrationBuilder.DropTable(
                name: "cartitem");

            migrationBuilder.DropTable(
                name: "clearanceitem");

            migrationBuilder.DropTable(
                name: "clearancelog");

            migrationBuilder.DropTable(
                name: "customer_choice");

            migrationBuilder.DropTable(
                name: "customerrewards");

            migrationBuilder.DropTable(
                name: "damagereport");

            migrationBuilder.DropTable(
                name: "deliverymethod");

            migrationBuilder.DropTable(
                name: "deposit");

            migrationBuilder.DropTable(
                name: "leg_carbon");

            migrationBuilder.DropTable(
                name: "lineitem");

            migrationBuilder.DropTable(
                name: "loanitem");

            migrationBuilder.DropTable(
                name: "loanlog");

            migrationBuilder.DropTable(
                name: "notification");

            migrationBuilder.DropTable(
                name: "notificationpreference");

            migrationBuilder.DropTable(
                name: "orderitem");

            migrationBuilder.DropTable(
                name: "orderstatushistory");

            migrationBuilder.DropTable(
                name: "packagingconfigmaterials");

            migrationBuilder.DropTable(
                name: "payment");

            migrationBuilder.DropTable(
                name: "plane");

            migrationBuilder.DropTable(
                name: "polineitem");

            migrationBuilder.DropTable(
                name: "pricing_rule");

            migrationBuilder.DropTable(
                name: "productdetails");

            migrationBuilder.DropTable(
                name: "productfootprint");

            migrationBuilder.DropTable(
                name: "purchaseorderlog");

            migrationBuilder.DropTable(
                name: "refund");

            migrationBuilder.DropTable(
                name: "reportexport");

            migrationBuilder.DropTable(
                name: "returnlog");

            migrationBuilder.DropTable(
                name: "ship");

            migrationBuilder.DropTable(
                name: "shipment");

            migrationBuilder.DropTable(
                name: "shipping_option");

            migrationBuilder.DropTable(
                name: "shipping_port");

            migrationBuilder.DropTable(
                name: "staffaccesslog");

            migrationBuilder.DropTable(
                name: "stafffootprint");

            migrationBuilder.DropTable(
                name: "suppliercategorychangelog");

            migrationBuilder.DropTable(
                name: "train");

            migrationBuilder.DropTable(
                name: "truck");

            migrationBuilder.DropTable(
                name: "vettingrecord");

            migrationBuilder.DropTable(
                name: "warehouse");

            migrationBuilder.DropTable(
                name: "return_stage");

            migrationBuilder.DropTable(
                name: "clearancebatch");

            migrationBuilder.DropTable(
                name: "returnitem");

            migrationBuilder.DropTable(
                name: "route_leg");

            migrationBuilder.DropTable(
                name: "carbon_result");

            migrationBuilder.DropTable(
                name: "replenishmentrequest");

            migrationBuilder.DropTable(
                name: "loanlist");

            migrationBuilder.DropTable(
                name: "packagingconfiguration");

            migrationBuilder.DropTable(
                name: "packagingmaterial");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "stockitem");

            migrationBuilder.DropTable(
                name: "ecobadge");

            migrationBuilder.DropTable(
                name: "purchaseorder");

            migrationBuilder.DropTable(
                name: "analytics");

            migrationBuilder.DropTable(
                name: "rentalorderlog");

            migrationBuilder.DropTable(
                name: "delivery_batch");

            migrationBuilder.DropTable(
                name: "staff");

            migrationBuilder.DropTable(
                name: "reliabilityrating");

            migrationBuilder.DropTable(
                name: "product_return");

            migrationBuilder.DropTable(
                name: "inventoryitem");

            migrationBuilder.DropTable(
                name: "returnrequest");

            migrationBuilder.DropTable(
                name: "route");

            migrationBuilder.DropTable(
                name: "transport");

            migrationBuilder.DropTable(
                name: "packagingprofile");

            migrationBuilder.DropTable(
                name: "transactionlog");

            migrationBuilder.DropTable(
                name: "supplier");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "transportation_hub");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "checkout");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "session");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
