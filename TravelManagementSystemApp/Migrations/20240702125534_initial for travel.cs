using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelManagementSystemApp.Migrations
{
    /// <inheritdoc />
    public partial class initialfortravel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addreses",
                columns: table => new
                {
                    AddressID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Street = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addreses", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Buses",
                columns: table => new
                {
                    BusID = table.Column<int>(name: "Bus_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusNumber = table.Column<int>(name: "Bus_Number", type: "int", maxLength: 20, nullable: false),
                    BusName = table.Column<string>(name: "Bus_Name", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FromLocation = table.Column<string>(name: "From_Location", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ToLocation = table.Column<string>(name: "To_Location", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalSeats = table.Column<int>(name: "Total_Seats", type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(name: "Available_Seats", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buses", x => x.BusID);
                });

            migrationBuilder.CreateTable(
                name: "Cabs",
                columns: table => new
                {
                    CabID = table.Column<int>(name: "Cab_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CabModel = table.Column<string>(name: "Cab_Model", type: "nvarchar(max)", nullable: false),
                    PickupLocation = table.Column<string>(name: "Pickup_Location", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DropoffLocation = table.Column<string>(name: "Dropoff_Location", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PickupTime = table.Column<DateTime>(name: "Pickup_Time", type: "datetime2", nullable: false),
                    DropoffTime = table.Column<DateTime>(name: "Dropoff_Time", type: "datetime2", nullable: true),
                    FareAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Distance = table.Column<double>(type: "float", nullable: false),
                    CabType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cabs", x => x.CabID);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    FlightID = table.Column<int>(name: "Flight_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(name: "Flight_Number", type: "nvarchar(max)", nullable: false),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureAirport = table.Column<string>(name: "Departure_Airport", type: "nvarchar(max)", nullable: false),
                    ArrivalAirport = table.Column<string>(name: "Arrival_Airport", type: "nvarchar(max)", nullable: false),
                    TotalSeats = table.Column<int>(name: "Total_Seats", type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(name: "Available_Seats", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.FlightID);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    EntityType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EntityID = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewID);
                });

            migrationBuilder.CreateTable(
                name: "Trains",
                columns: table => new
                {
                    TrainID = table.Column<int>(name: "Train_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainNumber = table.Column<int>(name: "Train_Number", type: "int", maxLength: 20, nullable: false),
                    TrainName = table.Column<string>(name: "Train_Name", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DepartureStation = table.Column<string>(name: "Departure_Station", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ArrivalStation = table.Column<string>(name: "Arrival_Station", type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalSeats = table.Column<int>(name: "Total_Seats", type: "int", nullable: false),
                    AvailableSeats = table.Column<int>(name: "Available_Seats", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trains", x => x.TrainID);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    userid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phonenumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.userid);
                });

            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    AirportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.AirportID);
                    table.ForeignKey(
                        name: "FK_Airports_Addreses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addreses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false),
                    StarRating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelID);
                    table.ForeignKey(
                        name: "FK_Hotels_Addreses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addreses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    StationID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    AddressID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.Code);
                    table.ForeignKey(
                        name: "FK_Stations_Addreses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addreses",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "busschedules",
                columns: table => new
                {
                    scheduleid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusId = table.Column<int>(type: "int", nullable: false),
                    Departuretime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Arrivaltime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_busschedules", x => x.scheduleid);
                    table.ForeignKey(
                        name: "FK_busschedules_Buses_BusId",
                        column: x => x.BusId,
                        principalTable: "Buses",
                        principalColumn: "Bus_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "flightschedules",
                columns: table => new
                {
                    scheduleid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    Departuretime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Arrivaltime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flightschedules", x => x.scheduleid);
                    table.ForeignKey(
                        name: "FK_flightschedules_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Flight_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trainschedules",
                columns: table => new
                {
                    scheduleid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainId = table.Column<int>(type: "int", nullable: false),
                    Departuretime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Arrivaltime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainschedules", x => x.scheduleid);
                    table.ForeignKey(
                        name: "FK_trainschedules_Trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "Trains",
                        principalColumn: "Train_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(name: "Booking_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userid = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(name: "Booking_Date", type: "datetime2", nullable: false),
                    BookingType = table.Column<string>(name: "Booking_Type", type: "nvarchar(max)", nullable: false),
                    BookingStatus = table.Column<string>(name: "Booking_Status", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_Bookings_users_userid",
                        column: x => x.userid,
                        principalTable: "users",
                        principalColumn: "userid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HotelID = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PricePerNight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                    table.ForeignKey(
                        name: "FK_Rooms_Hotels_HotelID",
                        column: x => x.HotelID,
                        principalTable: "Hotels",
                        principalColumn: "HotelID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Booking_Details",
                columns: table => new
                {
                    BookingDetailID = table.Column<int>(name: "Booking_Detail_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingID = table.Column<int>(name: "Booking_ID", type: "int", nullable: false),
                    DetailType = table.Column<string>(name: "Detail_Type", type: "nvarchar(max)", nullable: false),
                    DetailID = table.Column<int>(name: "Detail_ID", type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking_Details", x => x.BookingDetailID);
                    table.ForeignKey(
                        name: "FK_Booking_Details_Bookings_Booking_ID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "Booking_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(name: "Payment_ID", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingID = table.Column<int>(name: "Booking_ID", type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    PaymentDate = table.Column<DateTime>(name: "Payment_Date", type: "datetime2", nullable: false),
                    PaymentMethod = table.Column<string>(name: "Payment_Method", type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Bookings_Booking_ID",
                        column: x => x.BookingID,
                        principalTable: "Bookings",
                        principalColumn: "Booking_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Airports_AddressID",
                table: "Airports",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Details_Booking_ID",
                table: "Booking_Details",
                column: "Booking_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_userid",
                table: "Bookings",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_busschedules_BusId",
                table: "busschedules",
                column: "BusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_flightschedules_FlightId",
                table: "flightschedules",
                column: "FlightId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_AddressID",
                table: "Hotels",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_Booking_ID",
                table: "Payments",
                column: "Booking_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HotelID",
                table: "Rooms",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_Stations_AddressID",
                table: "Stations",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_trainschedules_TrainId",
                table: "trainschedules",
                column: "TrainId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "Booking_Details");

            migrationBuilder.DropTable(
                name: "busschedules");

            migrationBuilder.DropTable(
                name: "Cabs");

            migrationBuilder.DropTable(
                name: "flightschedules");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "trainschedules");

            migrationBuilder.DropTable(
                name: "Buses");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "Trains");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Addreses");
        }
    }
}
