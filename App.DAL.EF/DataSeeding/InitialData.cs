namespace App.DAL.EF.DataSeeding;

public static class InitialData
{
    public static readonly (string roleName, Guid? id)[]
        Roles =
        [
            ("admin", null),
            ("manager", null),
        ];


    public static readonly (string name, string firstName, string lastName, string password, Guid? id, string[] roles)[]
        Users =
        [
            ("admin@test.ee", "admin", "taltech", "Abc123-", null, ["admin"]),
            ("manager@test.ee", "manager", "taltech", "Abc123-", null, ["manager"]),
            ("user@test.ee", "user", "taltech", "Abc123-", null, []),
        ];
    
    public static readonly (string categoryName, string categoryDescription, Guid? id)[]
        Categories =
        [
            ("Electronics", "Devices, gadgets, and electronic components", null),
            ("Furniture", "Home and office furniture",  null),
            ("Clothing", "Apparel for men, women, and children", null),
            ("Food & Beverage", "Edible goods, beverages, and ingredients", null),
            ("Health & Beauty", "Personal care, cosmetics, and supplements", null),
            ("Sports & Outdoors", "Gear and equipment for sports and leisure", null),
            ("Automotive", "Auto parts, accessories, and tools", null),
            ("Office Supplies", "Stationery, printers, and office essentials", null),
        ];
    
    public static readonly (string productName, string productDescription, decimal productPrice, string productStatus, string categoryName, Guid? id)[]
        Products =
        [
            /* Electronics */
            ("Smartphone X1", "Latest-gen smartphone with OLED display", 699.99M, "ACTIVE", "Electronics", null),
            ("Laptop Pro 15", "Power-user laptop with 16 GB RAM & SSD", 1299.00M, "ACTIVE", "Electronics", null),
            ("Tablet S7", "Lightweight tablet with stylus support", 499.50M, "ACTIVE", "Electronics", null),
            ("Bluetooth Speaker Z", "Portable speaker with 12-hour battery life", 149.95M, "ACTIVE", "Electronics", null),
            ("Wireless Earbuds A2", "Noise-cancelling true-wireless earbuds", 199.99M, "ACTIVE", "Electronics", null),
            ("Smartwatch Series 5", "Fitness-focused smartwatch with GPS", 249.00M, "ACTIVE", "Electronics", null),
            ("Gaming Console Y", "Next-gen console with 4K/120 Hz support", 499.99M, "ACTIVE", "Electronics", null),
            ("4K Ultra HD Monitor", "27″ monitor with HDR and 1 ms response", 329.90M, "ACTIVE", "Electronics", null),
            ("DSLR Camera D3500", "Entry-level DSLR with 24 MP sensor", 449.00M, "ACTIVE", "Electronics", null),
            ("External SSD 1 TB", "Portable NVMe SSD, USB-C interface", 159.99M, "ACTIVE", "Electronics", null),
            ("Wi-Fi 6 Router", "Dual-band router with OFDMA support", 129.95M, "ACTIVE", "Electronics", null),
            ("VR Headset V3", "Immersive VR with hand-tracking", 399.00M, "ACTIVE", "Electronics", null),
            
            /* Furniture */
            ("Ergo Office Chair", "Adjustable mesh-back desk chair", 189.99M, "ACTIVE", "Furniture", null),
            ("Executive Desk", "L-shaped workstation, black wood finish", 349.50M, "ACTIVE", "Furniture",null),
            ("5-Shelf Bookshelf", "Industrial-style metal & wood shelving", 129.99M, "ACTIVE", "Furniture",null),
            ("Coffee Table Oak", "Solid oak rectangular coffee table", 199.00M, "ACTIVE", "Furniture",null),
            ("Dining Chair Set (4)", "Modern upholstered dining chairs", 279.00M, "ACTIVE", "Furniture",null),
            ("Sectional Sofa", "5-seat fabric sectional with chaise", 899.99M, "ACTIVE", "Furniture",null),
            ("Queen Bed Frame", "Upholstered platform bed with storage", 499.00M, "ACTIVE", "Furniture",null),
            ("Nightstand (2-Pack)", "Set of 2 birch wood beside tables", 159.95M, "ACTIVE", "Furniture",null),
            ("TV Media Console", "70″ TV stand with glass doors", 229.99M, "ACTIVE", "Furniture",null),
            ("File Cabinet 4-Draw", "Locking vertical file cabinet", 189.00M, "ACTIVE", "Furniture",null),
            ("Bar Stool – Set of 2", "Adjustable height swivel stools", 139.95M, "ACTIVE", "Furniture",null),
            ("Dresser 6-Drawers", "White wooden bedroom dresser", 299.99M, "ACTIVE", "Furniture",null),
            
            /* Clothing */
            ("Men’s Crew T-Shirt", "100% cotton short-sleeve tee", 19.99M, "ACTIVE", "Clothing", null),
            ("Women’s Skinny Jeans", "Stretch denim with high rise", 49.95M,  "ACTIVE","Clothing", null),
            ("Unisex Hoodie", "Fleece-lined pullover with kangaroo pocket", 39.99M, "ACTIVE", "Clothing",null),
            ("Running Sneakers", "Lightweight breathable trainers", 69.99M, "ACTIVE", "Clothing",null),
            ("Men’s Dress Shirt", "Slim-fit button-down, wrinkle-resistant", 34.99M, "ACTIVE", "Clothing",null),
            ("Women’s Jacket", "Water-resistant windbreaker", 59.99M, "ACTIVE", "Clothing",null),
            ("Yoga Leggings", "High-waist moisture-wicking leggings", 29.99M, "ACTIVE", "Clothing",null),
            ("Leather Belt", "Full-grain leather, brass buckle", 24.99M, "ACTIVE", "Clothing",null),
            ("Baseball Cap", "Adjustable cotton cap", 14.99M, "ACTIVE", "Clothing",null),
            ("Ankle Socks (5 pk)", "Cushioned cotton blend socks", 12.99M, "ACTIVE", "Clothing",null),
            ("Winter Coat", "Insulated parka with faux fur trim", 129.99M, "ACTIVE", "Clothing",null),
            ("Formal Dress", "Chiffon midi dress, A-line cut", 89.99M, "ACTIVE", "Clothing",null),
            
            /* Food & Beverage */
            ("Extra Virgin Olive Oil", "First-cold press, 1 L", 12.99M, "ACTIVE", "Food & Beverage",null),
            ("Organic Coffee Beans", "Whole bean, dark roast, 1 kg", 19.99M, "ACTIVE", "Food & Beverage",null),
            ("Green Tea Pack", "Sencha loose leaf, 200 g", 9.99M, "ACTIVE", "Food & Beverage",null),
            ("Protein Bar Variety", "12-pack assorted flavors", 24.99M, "ACTIVE", "Food & Beverage",null),
            ("Cold-Pressed Juice", "Beet & apple, 500 ml", 4.99M, "ACTIVE", "Food & Beverage",null),
            ("Canned Black Beans", "400 g BPA-free can", 1.99M, "ACTIVE", "Food & Beverage",null),
            ("Spaghetti Pasta", "Durum wheat semolina, 500 g", 2.49M, "ACTIVE", "Food & Beverage",null),
            ("Milk Chocolate Bo", "Assorted truffles, 200 g", 8.99M, "ACTIVE", "Food & Beverage",null),
            ("Breakfast Cereal", "Whole grain flakes, 500 g", 3.99M, "ACTIVE", "Food & Beverage",null),
            ("Energy Drink Pack", "4×250 ml cans, taurine blend", 6.99M, "ACTIVE", "Food & Beverage",null),
            ("Rice Basmati", "Premium aged, 1 kg", 5.49M, "ACTIVE", "Food & Beverage",null),
            ("Mixed Nuts Jar", "Salt-roasted almonds & cashews, 500 g", 14.99M, "ACTIVE", "Food & Beverage",null),
            ("Herb Spice Set", "Collection of 5 organic spices", 12.50M, "ACTIVE", "Food & Beverage",null),
            
            /* Health & Beauty */
            ("Hydrating Shampoo", "Sulfate-free botanical formula, 500 m", 15.99M, "ACTIVE", "Health & Beauty",null),
            ("Repair Conditioner", "Keratin-infused, 500 ml", 16.99M, "ACTIVE", "Health & Beauty",null),
            ("Body Lotion", "Shea butter & vitamin E, 300 ml", 12.99M, "ACTIVE", "Health & Beauty",null),
            ("Face Moisturizer", "SPF 15 day cream, 50 ml", 24.99M, "ACTIVE", "Health & Beauty",null),
            ("Electric Toothbrush", "Rechargeable with 3 brush heads", 39.99M, "ACTIVE", "Health & Beauty",null),
            ("Vitamin C Serum", "20% L-ascorbic acid, 30 ml", 29.99M, "ACTIVE", "Health & Beauty",null),
            ("Broad-Spectrum SPF", "Water-resistant sunscreen, 100 ml", 18.99M, "ACTIVE", "Health & Beauty",null),
            ("Lip Balm Trio", "Vanilla, mint, and cherry flavors", 7.99M, "ACTIVE", "Health & Beauty",null),
            ("Nail Polish Set", "6-color vegan nail ki", 19.99M, "ACTIVE", "Health & Beauty",null),
            ("Perfume Eau de Parfum", "50 ml floral signature scent", 49.99M, "ACTIVE", "Health & Beauty",null),
            ("Makeup Palette", "12-shade matte & shimmer", 34.99M, "ACTIVE", "Health & Beauty",null),
            ("Hand Sanitizer Gel", "70% alcohol, 500 ml pump bottle", 9.99M, "ACTIVE", "Health & Beauty",null),
            ("Hair Dryer Pro", "2000 W ionic with diffuser", 59.99M, "ACTIVE", "Health & Beauty",null),
            
            /* Sports & Outdoors */
            ("Tennis Racket Pro", "Graphite frame, strung", 119.99M, "ACTIVE", "Sports & Outdoors",null),
            ("Yoga Mat Deluxe", "Extra-thick non-slip EVA foam", 39.99M, "ACTIVE", "Sports & Outdoors",null),
            ("Hiking Backpack 40L", "Waterproof with rain cover", 89.99M, "ACTIVE", "Sports & Outdoors",null),
            ("Cycling Helmet", "Ventilated polycarbonate shell", 59.99M, "ACTIVE", "Sports & Outdoors",null),
            ("Insulated Water Bottle", "Stainless steel, 750 ml", 24.99M, "ACTIVE", "Sports & Outdoors",null),
            ("4-Person Tent", "Lightweight camping tent, 2 kg", 149.99M, "ACTIVE", "Sports & Outdoors",null),
            ("Dumbbell Set 20 kg", "Cast iron hex dumbbells", 79.99M, "ACTIVE", "Sports & Outdoors",null),
            ("Basketball Official", "Indoor/outdoor composite leather", 29.99M, "ACTIVE", "Sports & Outdoors",null),
            ("Soccer Ball Pro", "FIFA-approved match ball", 39.99M, "ACTIVE", "Sports & Outdoors",null),
            ("Fishing Rod & Reel", "Carbon-fiber spinning combo", 89.99M, "ACTIVE", "Sports & Outdoors",null),
            ("Running Shoes", "Breathable mesh with EVA midsole", 74.99M, "ACTIVE", "Sports & Outdoors",null),
            ("Camping Sleeping Bag", "Mummy bag rated to –5 °C", 69.99M, "ACTIVE", "Sports & Outdoors",null),
            
            /* Automative */
            ("Car Battery 12V", "Maintenance-free AGM battery", 129.99M, "ACTIVE", "Automotive",null),
            ("Engine Oil 5W-30", "Fully synthetic, 4 L can", 34.99M, "ACTIVE", "Automotive",null),
            ("Brake Pads (Front)", "Ceramic pads for sedans", 49.99M, "ACTIVE", "Automotive",null),
            ("Oil Filter Pack", "3-pack high-efficiency filter", 19.99M, "ACTIVE", "Automotive",null),
            ("Air Filter Replacement", "OEM spec pleated filter", 14.99M, "ACTIVE", "Automotive",null),
            ("Spark Plugs (4)", "Iridium tip performance plugs", 24.99M, "ACTIVE", "Automotive",null),
            ("Tire Inflator Kit", "12 V compressor & gauge", 39.99M, "ACTIVE", "Automotive",null),
            ("Windshield Wipers", "Pair of beam-blade wipers", 29.99M, "ACTIVE", "Automotive",null),
            ("Dash Cam Full HD", "Loop recording with night vision", 89.99M, "ACTIVE", "Automotive",null),
            ("Jump Starter 600A", "Portable lithium-ion power pack", 79.99M, "ACTIVE", "Automotive",null),
            ("Car Wax Premium", "Liquid carnauba wax, 500 ml", 19.99M, "ACTIVE", "Automotive",null),
            ("Leather Seat Covers", "Universal fit front & rear", 99.99M, "ACTIVE", "Automotive",null),
            ("All-Weather Floor Mats", "Custom fit rubber mats", 59.99M, "ACTIVE", "Automotive",null),
            
            /* Office Supplies */
            ("Ballpoint Pen – Pack", "20-pack medium-point pens", 9.99M, "ACTIVE", "Office Supplies",null),
            ("Spiral Notebook 5-Pack", "A4 ruled notebooks, 200 pages each", 14.99M, "ACTIVE", "Office Supplies",null),
            ("Printer Paper 500-Pack", "Multipurpose white paper, 80 gsm", 19.99M, "ACTIVE", "Office Supplies",null),
            ("Desktop Stapler", "Heavy-duty full-strip stapler", 12.99M, "ACTIVE", "Office Supplies",null),
            ("Binder Clips – Set", "Assorted sizes, 50-count", 6.99M, "ACTIVE", "Office Supplies",null),
            ("Highlighter Pen Se", "6 fluorescent colors", 7.99M, "ACTIVE", "Office Supplies",null),
            ("Desk Organizer", "4-compartment metal mesh", 24.99M, "ACTIVE", "Office Supplies",null),
            ("Whiteboard Markers", "8-color dry erase markers", 8.99M, "ACTIVE", "Office Supplies",null),
            ("File Folders 50-Pack", "Letter size assorted colors", 11.99M, "ACTIVE", "Office Supplies",null),
            ("Post-it Notes (12)", "3×3″ assorted colors, 12-pack", 10.99M, "ACTIVE", "Office Supplies",null),
            ("Envelope 100-Pack", "A2 invitation envelopes, white", 9.99M, "ACTIVE", "Office Supplies",null),
            ("Mouse Pad Ergonomic", "Gel wrist support pad", 14.99M, "ACTIVE", "Office Supplies",null),
            ("USB Flash Drive 64 GB", "USB-3.0 swivel flash drive", 15.99M, "ACTIVE", "Office Supplies",null)
        ];
    
    public static readonly (string supplierName, string supplierPhoneNumber, string supplierEmail, string supplierAddress, 
        string supplierStreet, string supplierCity, string supplierState, string supplierCountry, string supplierPostalCode, Guid? id)[]
        Suppliers =
        [
            ("GlobalTech Ltd", "+1-800-555-0101", "sales@globaltech.com", "123 Tech Drive, Silicon Valley, CA 94043", "123 Tech Drive", "Silicon Valley", "CA", "USA", "94043", null ),
            ("HomeComfort Inc", "+1-800-555-0202", "orders@homecomfort.com", "456 Maple Ave, Springfield, IL 62701","456 Maple Ave", "Springfield", "IL", "USA", "62701", null),
            ("FashionHub", "+1-800-555-0303", "contact@fashionhub.com", "789 Style Blvd, New York, NY 10001","789 Style Blvd", "New York", "NY", "USA", "10001", null),
            ("FreshHarvest Foods", "+1-800-555-0404", "sales@freshharvestfoods.com", "321 Farm Road, Des Moines, IA 50309", "321 Farm Road", "Des Moines", "IA", "USA", "50309",null),
            ("HealthPlus Pharma", "+1-800-555-0505", "info@healthpluspharma.com", "654 Wellness St, Boston, MA 02118", "654 Wellness St", "Boston", "MA", "USA", "02118",null),
            ("SportGear Co", "+1-800-555-0606", "support@sportgearco.com", "987 Stadium Way, Denver, CO 80205", "987 Stadium Way", "Denver", "CO", "USA", "80205",null),
            ("AutoParts Depot", "+1-800-555-0707", "parts@autopartsdepot.com", "159 Mechanic Ln, Detroit, MI 48201", "159 Mechanic Ln", "Detroit", "MI", "USA", "48201",null),
            ("OfficePro Services", "+1-800-555-0808", "service@officepro.com", "753 Paper Rd, Seattle, WA 98101", "753 Paper Rd", "Seattle", "WA", "USA", "98101",null),
            ("ElectroWorld Plus", "+1-800-555-1001", "contact@electroworldplus.com", "100 Tech Lane, Austin, TX 78701", "100 Tech Lane", "Austin", "TX", "USA", "78701",null),
            ("CircuitPro Components", "+1-800-555-1002", "sales@circuitpro.com", "200 Silicon Blvd, Austin, TX 78758", "200 Silicon Blvd", "Austin", "TX", "USA", "78758",null),
            ("GadgetGalaxy", "+1-800-555-1003", "info@gadgetgalaxy.com", "300 Innovation Drive, Palo Alto, CA 94301", "300 Innovation Drive", "Palo Alto", "CA", "USA", "94301",null),
            ("FurniLux Designs", "+1-800-555-2001", "support@furnilux.com", "123 Comfort St, Dallas, TX 75201", "123 Comfort St", "Dallas", "TX", "USA", "75201",null),
            ("UrbanChic Furnishings", "+1-800-555-2002", "sales@urbanchic.com", "456 Trendy Ave, Los Angeles, CA 90012", "456 Trendy Ave", "Los Angeles", "CA", "USA", "90012",null),
            ("StyleVista", "+1-800-555-3001", "contact@stylevista.com", "789 Fashion Blvd, New York, NY 10018", "789 Fashion Blvd", "New York", "NY", "USA", "10018",null),
            ("ApparelEdge", "+1-800-555-3002", "sales@appareledge.com", "321 Runway Rd, Miami, FL 33101", "321 Runway Rd", "Miami", "FL", "USA", "33101",null),
            ("GourmetHarvest", "+1-800-555-4001", "orders@gourmetharvest.com", "234 Farm Lane, Portland, OR 97201", "234 Farm Lane", "Portland", "OR", "USA", "97201",null),
            ("BeverageBarn", "+1-800-555-4002", "sales@beveragebarn.com", "567 Drink St, Seattle, WA 98109", "567 Drink St", "Seattle", "WA", "USA", "98109",null),
            ("BeautyEssence", "+1-800-555-5001", "info@beautyessence.com", "89 Spa Rd, Scottsdale, AZ 85250", "89 Spa Rd", "Scottsdale", "AZ", "USA", "85250",null),
            ("HealthHarvest", "+1-800-555-5002", "orders@healthharvest.com", "123 Wellness Way, Austin, TX 78759", "123 Wellness Way", "Austin", "TX", "USA", "78759",null),
            ("PeakPerformance Gear", "+1-800-555-6001", "support@peakperformance.com", "234 Summit Blvd, Boulder, CO 80302", "234 Summit Blvd", "Boulder", "CO", "USA", "80302",null),
            ("OutdoorMaster", "+1-800-555-6002", "sales@outdoormaster.com", "345 Trail Rd, Denver, CO 80204", "345 Trail Rd", "Denver", "CO", "USA", "80204",null),
            ("AutoPro Parts", "+1-800-555-7001", "contact@autoproparts.com", "456 Garage St, Detroit, MI 48202", "456 Garage St", "Detroit", "MI", "USA", "48202",null),
            ("DriveTech", "+1-800-555-7002", "sales@drivetech.com", "789 Mechanic Ave, Cleveland, OH 44114", "789 Mechanic Ave", "Cleveland", "OH", "USA", "44114",null),
            ("PaperPlus Supplies", "+1-800-555-8001", "orders@paperplus.com", "890 Office Park, Boston, MA 02109", "890 Office Park", "Boston", "MA", "USA", "02109",null),
            ("StationeryWorld", "+1-800-555-8002", "sales@stationeryworld.com", "123 Pen Rd, Chicago, IL 60601", "123 Pen Rd", "Chicago", "IL", "USA", "60601",null),
        ];
    
        
    public static readonly (string warehouseAddress, string warehouseStreet,  string warehouseCity, string warehouserState, 
    string warehouseCountry, string warehousePostalCode,string warehouseEmail, int warehouseCapacity, Guid? id)[]
        Warehouses =
        [
            ("100 Industrial Road, Chicago, IL 60607", "100 Industrial Road", "Chicago", "IL", "USA", "60607",   "wh-chicago@warehouseco.com",      5000, null),
            ("250 Logistics Blvd, Atlanta, GA 30303", "250 Logistics Blvd", "Atlanta", "GA", "USA", "30303",    "wh-atlanta@warehouseco.com",      4500, null),
            ("75 Portside Drive, Long Beach, CA 90802", "75 Portside Drive", "Long Beach", "CA", "USA", "90802",  "wh-longbeach@warehouseco.com",    6000, null),
            ("12 Distribution Lane, Dallas, TX 75201", "12 Distribution Lane", "Dallas", "TX", "USA", "75201",   "wh-dallas@warehouseco.com",       4000, null),
            ("5000 Supply Way, Seattle, WA 98101", "5000 Supply Way", "Seattle", "WA", "USA", "98101",      "wh-seattle@warehouseco.com",      5500, null),
            ("8000 Storage Park, Orlando, FL 32801", "8000 Storage Park", "Orlando", "FL", "USA", "32801",    "wh-orlando@warehouseco.com",      3000, null),
            ("330 Freight Avenue, New York, NY 10001","330 Freight Avenue", "New York", "NY", "USA", "10001",   "wh-nyc@warehouseco.com",          7000, null),
            ("1200 Cargo Road, Memphis, TN 38103", "1200 Cargo Road", "Memphis", "TN", "USA", "38103",      "wh-memphis@warehouseco.com",      4800, null),
            ("4500 Inland Empire, San Bernardino, CA 38104","4500 Inland Empire", "San Bernardino", "CA", "USA", "38104",   "wh-sb@warehouseco.com",           5200, null),
            ("200 Bay Street, Newark, NJ 07102", "200 Bay Street", "Newark", "NJ", "USA", "07102",        "wh-newark@warehouseco.com",       6500, null),
        ];
}  