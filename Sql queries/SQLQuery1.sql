INSERT INTO Customers (Name, Address, Email, PhoneNumber, CreatedAt, UpdatedAt, DeletedAt)
VALUES
('AzerTech LLC', 'Baku, Heydar Aliyev ave 15', 'info@azertech.az', '+994501111111', '2024-01-10', '2024-01-10', NULL),
('Caspian Logistics', 'Baku, Nobel ave 23', 'contact@caspianlog.az', '+994502222222', '2024-01-12', '2024-01-12', NULL),
('GreenLine Construction', 'Baku, Yasamal district 5', 'office@greenline.az', '+994503333333', '2024-02-01', '2024-02-01', NULL),
('Smart Solutions', 'Baku, Nizami st 44', 'hello@smart.az', '+994504444444', '2024-02-10', '2024-02-10', NULL),
('Atlas Travel', 'Baku, Fountains Square 2', 'sales@atlas.az', '+994505555555', '2024-02-20', '2024-02-20', NULL),
('Baku Electronics', 'Baku, 28 May st 10', 'support@bakuelec.az', '+994506666666', '2024-03-05', '2024-03-05', NULL),
('Golden Foods', 'Baku, Babek ave 78', 'info@goldenfoods.az', '+994507777777', '2024-03-15', '2024-03-15', NULL),
('Oil Services Group', 'Baku, Khatai ave 12', 'contact@oilservices.az', '+994508888888', '2024-04-01', '2024-04-01', NULL),
('Mountain Hotel', 'Guba, City center 1', 'booking@mountain.az', '+994509999999', '2024-04-10', '2024-04-10', NULL),
('Digital Marketing Pro', 'Baku, Port Baku Tower', 'admin@dmp.az', '+994501234567', '2024-05-01', '2024-05-01', NULL);

INSERT INTO Invoices
(CustomerId, StartDate, EndDate, TotalSum, Comment, Status, CreatedAt, UpdatedAt, DeletedAt)
VALUES
(2, '2024-06-01', '2024-06-10', 0, 'Website development', 0, '2024-06-10', '2024-06-10', NULL),
(2, '2024-07-01', '2024-07-05', 0, 'Support services', 3, '2024-07-06', '2024-07-06', NULL),
(3, '2024-06-15', '2024-06-20', 0, 'Transportation services', 1, '2024-06-21', '2024-06-21', NULL),
(3, '2024-07-10', '2024-07-15', 0, 'Warehouse rent', 2, '2024-07-16', '2024-07-16', NULL),
(4, '2024-05-01', '2024-05-30', 0, 'Building materials', 3, '2024-06-01', '2024-06-01', NULL),
(4, '2024-06-01', '2024-06-15', 0, 'Construction work', 4, '2024-06-16', '2024-06-16', NULL),
(5, '2024-04-10', '2024-04-20', 0, 'IT consulting', 3, '2024-04-21', '2024-04-21', NULL),
(5, '2024-05-05', '2024-05-10', 0, 'System integration', 5, '2024-05-11', '2024-05-11', NULL),
(6, '2024-03-01', '2024-03-07', 0, 'Tour package', 3, '2024-03-08', '2024-03-08', NULL),
(6, '2024-04-01', '2024-04-10', 0, 'Corporate travel', 1, '2024-04-11', '2024-04-11', NULL),

(7, '2024-02-01', '2024-02-05', 0, 'Electronics supply', 3, '2024-02-06', '2024-02-06', NULL),
(7, '2024-03-01', '2024-03-10', 0, 'Repair service', 2, '2024-03-11', '2024-03-11', NULL),
(8, '2024-05-01', '2024-05-15', 0, 'Food delivery', 3, '2024-05-16', '2024-05-16', NULL),
(8, '2024-06-01', '2024-06-05', 0, 'Catering service', 0, '2024-06-06', '2024-06-06', NULL),
(9, '2024-01-10', '2024-01-20', 0, 'Equipment maintenance', 3, '2024-01-21', '2024-01-21', NULL),
(9, '2024-02-10', '2024-02-15', 0, 'Oil field support', 1, '2024-02-16', '2024-02-16', NULL),

(10, '2024-03-01', '2024-03-03', 0, 'Hotel accommodation', 3, '2024-03-04', '2024-03-04', NULL),
(10, '2024-04-01', '2024-04-05', 0, 'Conference hall rent', 2, '2024-04-06', '2024-04-06', NULL),
(11, '2024-05-01', '2024-05-30', 0, 'Marketing campaign', 3, '2024-05-31', '2024-05-31', NULL),
(11, '2024-06-01', '2024-06-15', 0, 'SEO services', 1, '2024-06-16', '2024-06-16', NULL),

(2, '2024-08-01', '2024-08-10', 0, 'Server maintenance', 3, '2024-08-11', '2024-08-11', NULL),
(3, '2024-08-05', '2024-08-10', 0, 'Logistics planning', 0, '2024-08-11', '2024-08-11', NULL),
(4, '2024-07-01', '2024-07-20', 0, 'Renovation work', 3, '2024-07-21', '2024-07-21', NULL),
(5, '2024-06-10', '2024-06-15', 0, 'Cloud setup', 1, '2024-06-16', '2024-06-16', NULL),
(6, '2024-07-01', '2024-07-05', 0, 'VIP travel package', 3, '2024-07-06', '2024-07-06', NULL),
(7, '2024-07-10', '2024-07-12', 0, 'Device repair', 5, '2024-07-13', '2024-07-13', NULL),
(8, '2024-08-01', '2024-08-03', 0, 'Office catering', 3, '2024-08-04', '2024-08-04', NULL),
(9, '2024-09-01', '2024-09-10', 0, 'Technical inspection', 0, '2024-09-11', '2024-09-11', NULL),
(10, '2024-09-15', '2024-09-20', 0, 'Group accommodation', 3, '2024-09-21', '2024-09-21', NULL),
(11, '2024-10-01', '2024-10-15', 0, 'Social media ads', 2, '2024-10-16', '2024-10-16', NULL);

INSERT INTO InvoiceRows (InvoiceId, Service, Quantity, Rate, Sum)
VALUES
(3, 'Frontend development', 40, 25, 1000),
(3, 'Backend development', 30, 30, 900),
(4, 'Monthly support', 1, 300, 300),
(5, 'Cargo delivery', 5, 120, 600),
(5, 'Insurance', 1, 200, 200),

(6, 'Storage rent', 1, 500, 500),
(7, 'Cement supply', 100, 8, 800),
(7, 'Steel supply', 50, 15, 750),
(9, 'Consulting hours', 10, 40, 400),
(10, 'Integration setup', 1, 600, 600),

(11, 'Tour package', 2, 700, 1400),
(12, 'Flight tickets', 3, 350, 1050),
(13, 'Laptop supply', 5, 800, 4000),
(14, 'Repair service', 3, 120, 360),
(15, 'Food delivery', 20, 25, 500),

(16, 'Catering event', 1, 900, 900),
(17, 'Maintenance work', 10, 50, 500),
(18, 'Field support', 8, 70, 560),
(19, 'Hotel room', 3, 150, 450),
(20, 'Conference hall', 1, 1000, 1000),

(21, 'Google Ads', 1, 2000, 2000),
(22, 'SEO optimization', 1, 1200, 1200),
(23, 'Server support', 1, 800, 800),
(24, 'Logistics plan', 1, 400, 400),
(25, 'Renovation materials', 1, 1500, 1500),

(26, 'Cloud configuration', 1, 900, 900),
(27, 'VIP package', 2, 1000, 2000),
(28, 'Phone repair', 2, 150, 300),
(29, 'Office lunch', 15, 20, 300),
(30, 'Inspection service', 1, 700, 700),

(31, 'Group booking', 5, 180, 900),
(32, 'Instagram Ads', 1, 600, 600),
(32, 'Facebook Ads', 1, 500, 500),
(21, 'Content creation', 10, 50, 500),
(7, 'Transport materials', 3, 200, 600),

(9, 'System audit', 1, 350, 350),
(11, 'Extra luggage', 2, 60, 120),
(13, 'Printer supply', 2, 250, 500),
(14, 'Parts replacement', 4, 90, 360),
(16, 'Desserts catering', 1, 200, 200),

(17, 'Equipment check', 5, 60, 300),
(19, 'Breakfast service', 3, 40, 120),
(23, 'Backup setup', 1, 300, 300),
(25, 'Painting work', 1, 700, 700),
(27, 'Hotel transfer', 1, 120, 120),

(29, 'Extra snacks', 10, 10, 100),
(30, 'Safety audit', 1, 500, 500),
(31, 'Dinner service', 5, 50, 250),
(32, 'Targeting setup', 1, 200, 200),
(22, 'Technical SEO audit', 1, 600, 600);