using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CricketBiddingApp.Api.Migrations
{
    /// <inheritdoc />
    public partial class postgresql_migration1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //    migrationBuilder.CreateTable(
            //        name: "Auction",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "integer", nullable: false)
            //                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //            Name = table.Column<string>(type: "text", nullable: false),
            //            TotalBudget = table.Column<decimal>(type: "numeric", nullable: false),
            //            StartDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //            EndDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Auction", x => x.Id);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Team",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "integer", nullable: false)
            //                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //            Name = table.Column<string>(type: "text", nullable: false),
            //            Budget = table.Column<decimal>(type: "numeric", nullable: false),
            //            MaxPlayers = table.Column<int>(type: "integer", nullable: false),
            //            AuctionId = table.Column<int>(type: "integer", nullable: false)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Team", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Team_Auction_AuctionId",
            //                column: x => x.AuctionId,
            //                principalTable: "Auction",
            //                principalColumn: "Id",
            //                onDelete: ReferentialAction.Cascade);
            //        });

            //    migrationBuilder.CreateTable(
            //        name: "Player",
            //        columns: table => new
            //        {
            //            Id = table.Column<int>(type: "integer", nullable: false)
            //                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //            Name = table.Column<string>(type: "text", nullable: false),
            //            Category = table.Column<string>(type: "text", nullable: false),
            //            StaffType = table.Column<string>(type: "text", nullable: false),
            //            IsSold = table.Column<bool>(type: "boolean", nullable: false),
            //            CurrentBid = table.Column<decimal>(type: "numeric", nullable: false),
            //            WinningTeamId = table.Column<int>(type: "integer", nullable: false),
            //            CategoryId = table.Column<int>(type: "integer", nullable: false),
            //            AuctionId = table.Column<int>(type: "integer", nullable: true),
            //            TeamId = table.Column<int>(type: "integer", nullable: true)
            //        },
            //        constraints: table =>
            //        {
            //            table.PrimaryKey("PK_Player", x => x.Id);
            //            table.ForeignKey(
            //                name: "FK_Player_Auction_AuctionId",
            //                column: x => x.AuctionId,
            //                principalTable: "Auction",
            //                principalColumn: "Id");
            //            table.ForeignKey(
            //                name: "FK_Player_Team_TeamId",
            //                column: x => x.TeamId,
            //                principalTable: "Team",
            //                principalColumn: "Id");
            //        });

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Player_AuctionId",
            //        table: "Player",
            //        column: "AuctionId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Player_TeamId",
            //        table: "Player",
            //        column: "TeamId");

            //    migrationBuilder.CreateIndex(
            //        name: "IX_Team_AuctionId",
            //        table: "Team",
            //        column: "AuctionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "Auction");
        }
    }
}
