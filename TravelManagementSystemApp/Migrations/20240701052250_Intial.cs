using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelManagementSystemApp.Migrations
{
    /// <inheritdoc />
    public partial class Intial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "buses",
                columns: table => new
                {
                    Bus_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bus_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departure_Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arrival_Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total_Seats = table.Column<int>(type: "int", nullable: false),
                    Available_Seats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_buses", x => x.Bus_ID);
                });

            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    Flight_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Flight_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departure_Airport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arrival_Airport = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total_Seats = table.Column<int>(type: "int", nullable: false),
                    Available_Seats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.Flight_ID);
                });

            migrationBuilder.CreateTable(
                name: "trains",
                columns: table => new
                {
                    Train_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Train_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Train_Operator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departure_Station = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Arrival_Station = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Total_Seats = table.Column<int>(type: "int", nullable: false),
                    Available_Seats = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trains", x => x.Train_ID);
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
                        name: "FK_busschedules_buses_BusId",
                        column: x => x.BusId,
                        principalTable: "buses",
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
                        name: "FK_flightschedules_flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "flights",
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
                        name: "FK_trainschedules_trains_TrainId",
                        column: x => x.TrainId,
                        principalTable: "trains",
                        principalColumn: "Train_ID",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_trainschedules_TrainId",
                table: "trainschedules",
                column: "TrainId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "busschedules");

            migrationBuilder.DropTable(
                name: "flightschedules");

            migrationBuilder.DropTable(
                name: "trainschedules");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "buses");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "trains");
        }
    }
}
