
-- ============================================================
-- TRANSPORTATION HUB SEED (PostgreSQL)
-- Uses CTEs + RETURNING to safely capture auto-generated hub_ids
-- ============================================================

WITH inserted_airports AS (
    INSERT INTO transportation_hub (hub_type, longitude, latitude, country_code, address, operational_status, operation_time)
    VALUES
        ('AIRPORT',  103.9894,   1.3644, 'SG', 'Singapore Changi Airport, 60 Airport Boulevard, Singapore 819643',      'OPERATIONAL', '24/7'),
        ('AIRPORT',  113.9145,  22.3089, 'HK', 'Hong Kong International Airport, Lantau, Hong Kong',               'OPERATIONAL', '24/7'),
        ('AIRPORT',  140.3929,  35.7720, 'JP', '1 Furugome, Narita, Chiba 282-0004, Japan',                        'OPERATIONAL', '24/7'),
        ('AIRPORT',   55.3644,  25.2532, 'AE', 'Dubai International Airport, Airport Road, Dubai, UAE',            'OPERATIONAL', '24/7'),
        ('AIRPORT',   -0.4543,  51.4700, 'GB', 'Heathrow Airport, Longford, Hounslow TW6 1AP, United Kingdom',     'OPERATIONAL', '24/7'),
        ('AIRPORT', -118.4085,  33.9425, 'US', '1 World Way, Los Angeles, CA 90045, USA',                          'OPERATIONAL', '24/7')
    RETURNING hub_id, address
),
inserted_airport_subtypes AS (
    INSERT INTO airport (hub_id, airport_code, airport_name, terminal, aircraft_size)
    SELECT
        hub_id,
        CASE address
            WHEN 'Singapore Changi Airport, 60 Airport Boulevard, Singapore 819643' THEN 'SIN'
            WHEN 'Hong Kong International Airport, Lantau, Hong Kong'           THEN 'HKG'
            WHEN '1 Furugome, Narita, Chiba 282-0004, Japan'                   THEN 'NRT'
            WHEN 'Dubai International Airport, Airport Road, Dubai, UAE'        THEN 'DXB'
            WHEN 'Heathrow Airport, Longford, Hounslow TW6 1AP, United Kingdom' THEN 'LHR'
            WHEN '1 World Way, Los Angeles, CA 90045, USA'                      THEN 'LAX'
        END,
        CASE address
            WHEN 'Singapore Changi Airport, 60 Airport Boulevard, Singapore 819643' THEN 'Singapore Changi Airport'
            WHEN 'Hong Kong International Airport, Lantau, Hong Kong'           THEN 'Hong Kong International Airport'
            WHEN '1 Furugome, Narita, Chiba 282-0004, Japan'                   THEN 'Narita International Airport'
            WHEN 'Dubai International Airport, Airport Road, Dubai, UAE'        THEN 'Dubai International Airport'
            WHEN 'Heathrow Airport, Longford, Hounslow TW6 1AP, United Kingdom' THEN 'Heathrow Airport'
            WHEN '1 World Way, Los Angeles, CA 90045, USA'                      THEN 'Los Angeles International Airport'
        END,
        CASE address
            WHEN 'Singapore Changi Airport, 60 Airport Boulevard, Singapore 819643' THEN 3
            WHEN 'Hong Kong International Airport, Lantau, Hong Kong'           THEN 1
            WHEN '1 Furugome, Narita, Chiba 282-0004, Japan'                   THEN 2
            WHEN 'Dubai International Airport, Airport Road, Dubai, UAE'        THEN 3
            WHEN 'Heathrow Airport, Longford, Hounslow TW6 1AP, United Kingdom' THEN 5
            WHEN '1 World Way, Los Angeles, CA 90045, USA'                      THEN 4
        END,
        CASE address
            WHEN 'Singapore Changi Airport, 60 Airport Boulevard, Singapore 819643' THEN 620
            WHEN 'Hong Kong International Airport, Lantau, Hong Kong'           THEN 550
            WHEN '1 Furugome, Narita, Chiba 282-0004, Japan'                   THEN 450
            WHEN 'Dubai International Airport, Airport Road, Dubai, UAE'        THEN 600
            WHEN 'Heathrow Airport, Longford, Hounslow TW6 1AP, United Kingdom' THEN 520
            WHEN '1 World Way, Los Angeles, CA 90045, USA'                      THEN 500
        END
    FROM inserted_airports
),

-- ---- SHIPPING PORTS ----
inserted_ports AS (
    INSERT INTO transportation_hub (hub_type, longitude, latitude, country_code, address, operational_status, operation_time)
    VALUES
        ('SHIPPING_PORT',  103.6266,   1.2400, 'SG', 'PSA Tuas Port, 31 Tuas View Extension, Singapore 637434',          'OPERATIONAL', '6AM-10PM'),
        ('SHIPPING_PORT',  114.1211,  22.3694, 'HK', 'Kwai Tsing Container Terminal, Kwai Chung, Hong Kong',              'OPERATIONAL', '6AM-10PM'),
        ('SHIPPING_PORT',  139.7798,  35.6167, 'JP', 'Tokyo International Container Terminal, Aomi, Koto City, Tokyo 135-0064, Japan', 'OPERATIONAL', '6AM-10PM'),
        ('SHIPPING_PORT',   55.0200,  24.9857, 'AE', 'Jebel Ali Port, Mina Jebel Ali, Dubai, UAE',                        'OPERATIONAL', '6AM-10PM'),
        ('SHIPPING_PORT',    1.3132,  51.9553, 'GB', 'Port of Felixstowe, Dock Road, Felixstowe IP11 3SY, United Kingdom','OPERATIONAL', '6AM-10PM'),
        ('SHIPPING_PORT',    4.0513,  51.9225, 'NL', 'Maasvlakte 2, 3199 LA Rotterdam, Netherlands',                      'OPERATIONAL', '6AM-10PM'),
        ('SHIPPING_PORT',  121.8095,  30.6261, 'CN', 'Yangshan Deep Water Port, Shengsi, Zhoushan, Zhejiang, China',      'OPERATIONAL', '6AM-10PM'),
        ('SHIPPING_PORT', -118.2702,  33.7295, 'US', '425 S Palos Verdes Street, San Pedro, CA 90731, USA',               'OPERATIONAL', '6AM-10PM')
    RETURNING hub_id, address
),
inserted_port_subtypes AS (
    INSERT INTO shipping_port (hub_id, port_code, port_name, port_type, vessel_size)
    SELECT
        hub_id,
        CASE address
            WHEN 'PSA Tuas Port, 31 Tuas View Extension, Singapore 637434'       THEN 'SG-TUAS'
            WHEN 'Kwai Tsing Container Terminal, Kwai Chung, Hong Kong'         THEN 'HK-KWAI'
            WHEN 'Tokyo International Container Terminal, Aomi, Koto City, Tokyo 135-0064, Japan' THEN 'JP-TOKYO'
            WHEN 'Jebel Ali Port, Mina Jebel Ali, Dubai, UAE'                   THEN 'AE-JEA'
            WHEN 'Port of Felixstowe, Dock Road, Felixstowe IP11 3SY, United Kingdom' THEN 'GB-FELIX'
            WHEN 'Maasvlakte 2, 3199 LA Rotterdam, Netherlands'                 THEN 'NL-RTM'
            WHEN 'Yangshan Deep Water Port, Shengsi, Zhoushan, Zhejiang, China' THEN 'CN-SHA'
            WHEN '425 S Palos Verdes Street, San Pedro, CA 90731, USA'          THEN 'US-LAPORT'
        END,
        CASE address
            WHEN 'PSA Tuas Port, 31 Tuas View Extension, Singapore 637434'       THEN 'PSA Tuas Port'
            WHEN 'Kwai Tsing Container Terminal, Kwai Chung, Hong Kong'         THEN 'Kwai Tsing Container Terminal'
            WHEN 'Tokyo International Container Terminal, Aomi, Koto City, Tokyo 135-0064, Japan' THEN 'Tokyo International Container Terminal'
            WHEN 'Jebel Ali Port, Mina Jebel Ali, Dubai, UAE'                   THEN 'Jebel Ali Port'
            WHEN 'Port of Felixstowe, Dock Road, Felixstowe IP11 3SY, United Kingdom' THEN 'Port of Felixstowe'
            WHEN 'Maasvlakte 2, 3199 LA Rotterdam, Netherlands'                 THEN 'Port of Rotterdam'
            WHEN 'Yangshan Deep Water Port, Shengsi, Zhoushan, Zhejiang, China' THEN 'Port of Shanghai - Yangshan'
            WHEN '425 S Palos Verdes Street, San Pedro, CA 90731, USA'          THEN 'Port of Los Angeles'
        END,
        'CONTAINER_PORT',
        CASE address
            WHEN 'PSA Tuas Port, 31 Tuas View Extension, Singapore 637434'       THEN 9500
            WHEN 'Kwai Tsing Container Terminal, Kwai Chung, Hong Kong'         THEN 4800
            WHEN 'Tokyo International Container Terminal, Aomi, Koto City, Tokyo 135-0064, Japan' THEN 7200
            WHEN 'Jebel Ali Port, Mina Jebel Ali, Dubai, UAE'                   THEN 8800
            WHEN 'Port of Felixstowe, Dock Road, Felixstowe IP11 3SY, United Kingdom' THEN 7600
            WHEN 'Maasvlakte 2, 3199 LA Rotterdam, Netherlands'                 THEN 8000
            WHEN 'Yangshan Deep Water Port, Shengsi, Zhoushan, Zhejiang, China' THEN 9000
            WHEN '425 S Palos Verdes Street, San Pedro, CA 90731, USA'          THEN 7000
        END
    FROM inserted_ports
),

-- ---- WAREHOUSES ----
inserted_warehouses AS (
    INSERT INTO transportation_hub (hub_type, longitude, latitude, country_code, address, operational_status, operation_time)
    VALUES
        ('WAREHOUSE',  103.6395,   1.3290, 'SG', 'Jurong Logistics Hub, 1 Buroh Crescent, Singapore 627545',              'OPERATIONAL', '8AM-8PM'),
        ('WAREHOUSE',  139.7794,  35.5494, 'JP', '2-1 Hanedakuko, Ota City, Tokyo 144-0041, Japan',                       'OPERATIONAL', '8AM-8PM'),
        ('WAREHOUSE',   55.0273,  24.8964, 'AE', 'Dubai Logistics City, Al Maktoum International Airport Road, Dubai, UAE','OPERATIONAL', '8AM-8PM'),
        ('WAREHOUSE',   -0.4050,  51.5002, 'GB', 'Colonial Way, Watford, Hertfordshire WD24 4PT, United Kingdom',          'OPERATIONAL', '8AM-8PM'),
        ('WAREHOUSE', -118.2083,  33.8115, 'US', '2401 E Artesia Boulevard, Long Beach, CA 90805, USA',                    'OPERATIONAL', '8AM-8PM')
    RETURNING hub_id, address
)
INSERT INTO warehouse (hub_id, warehouse_code, max_product_capacity, total_warehouse_volume, climate_control_emission_rate, lighting_emission_rate, security_system_emission_rate)
SELECT
    hub_id,
    CASE address
        WHEN 'Jurong Logistics Hub, 1 Buroh Crescent, Singapore 627545'                    THEN 'WH-SG-001'
        WHEN '2-1 Hanedakuko, Ota City, Tokyo 144-0041, Japan'                        THEN 'WH-JP-001'
        WHEN 'Dubai Logistics City, Al Maktoum International Airport Road, Dubai, UAE' THEN 'WH-AE-001'
        WHEN 'Colonial Way, Watford, Hertfordshire WD24 4PT, United Kingdom'           THEN 'WH-GB-001'
        WHEN '2401 E Artesia Boulevard, Long Beach, CA 90805, USA'                     THEN 'WH-US-001'
    END,
    CASE address
        WHEN 'Jurong Logistics Hub, 1 Buroh Crescent, Singapore 627545'                    THEN 11000
        WHEN '2-1 Hanedakuko, Ota City, Tokyo 144-0041, Japan'                        THEN 9000
        WHEN 'Dubai Logistics City, Al Maktoum International Airport Road, Dubai, UAE' THEN 7500
        WHEN 'Colonial Way, Watford, Hertfordshire WD24 4PT, United Kingdom'           THEN 8500
        WHEN '2401 E Artesia Boulevard, Long Beach, CA 90805, USA'                     THEN 12000
    END,
    CASE address
        WHEN 'Jurong Logistics Hub, 1 Buroh Crescent, Singapore 627545'                    THEN 5200.0
        WHEN '2-1 Hanedakuko, Ota City, Tokyo 144-0041, Japan'                        THEN 4500.0
        WHEN 'Dubai Logistics City, Al Maktoum International Airport Road, Dubai, UAE' THEN 3800.0
        WHEN 'Colonial Way, Watford, Hertfordshire WD24 4PT, United Kingdom'           THEN 4200.0
        WHEN '2401 E Artesia Boulevard, Long Beach, CA 90805, USA'                     THEN 6000.0
    END,
    CASE address
        WHEN 'Jurong Logistics Hub, 1 Buroh Crescent, Singapore 627545'                    THEN 2.4
        WHEN '2-1 Hanedakuko, Ota City, Tokyo 144-0041, Japan'                        THEN 2.1
        WHEN 'Dubai Logistics City, Al Maktoum International Airport Road, Dubai, UAE' THEN 3.0
        WHEN 'Colonial Way, Watford, Hertfordshire WD24 4PT, United Kingdom'           THEN 2.3
        WHEN '2401 E Artesia Boulevard, Long Beach, CA 90805, USA'                     THEN 2.0
    END,
    CASE address
        WHEN 'Jurong Logistics Hub, 1 Buroh Crescent, Singapore 627545'                    THEN 1.8
        WHEN '2-1 Hanedakuko, Ota City, Tokyo 144-0041, Japan'                        THEN 1.5
        WHEN 'Dubai Logistics City, Al Maktoum International Airport Road, Dubai, UAE' THEN 1.9
        WHEN 'Colonial Way, Watford, Hertfordshire WD24 4PT, United Kingdom'           THEN 1.7
        WHEN '2401 E Artesia Boulevard, Long Beach, CA 90805, USA'                     THEN 1.6
    END,
    CASE address
        WHEN 'Jurong Logistics Hub, 1 Buroh Crescent, Singapore 627545'                    THEN 0.5
        WHEN '2-1 Hanedakuko, Ota City, Tokyo 144-0041, Japan'                        THEN 0.4
        WHEN 'Dubai Logistics City, Al Maktoum International Airport Road, Dubai, UAE' THEN 0.5
        WHEN 'Colonial Way, Watford, Hertfordshire WD24 4PT, United Kingdom'           THEN 0.4
        WHEN '2401 E Artesia Boulevard, Long Beach, CA 90805, USA'                     THEN 0.5
    END
FROM inserted_warehouses;
