using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Quartz.Core.Migrations
{
    public partial class TileMaps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DirectionMaps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    East = table.Column<bool>(nullable: false),
                    North = table.Column<bool>(nullable: false),
                    South = table.Column<bool>(nullable: false),
                    West = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectionMaps", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TileMaps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    GridSize = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TileMaps", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CollisionMaps",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CanEnterFromID = table.Column<int>(nullable: true),
                    CanExitToID = table.Column<int>(nullable: true),
                    IsBush = table.Column<bool>(nullable: false),
                    IsLadder = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollisionMaps", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CollisionMaps_DirectionMaps_CanEnterFromID",
                        column: x => x.CanEnterFromID,
                        principalTable: "DirectionMaps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CollisionMaps_DirectionMaps_CanExitToID",
                        column: x => x.CanExitToID,
                        principalTable: "DirectionMaps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MapLayers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    ZOrder = table.Column<int>(nullable: false),
                    TileMapID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapLayers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MapLayers_TileMaps_TileMapID",
                        column: x => x.TileMapID,
                        principalTable: "TileMaps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TileBases",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    CollisionMapID = table.Column<int>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TileBases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TileBases_CollisionMaps_CollisionMapID",
                        column: x => x.CollisionMapID,
                        principalTable: "CollisionMaps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tiles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    BasedOnID = table.Column<int>(nullable: false),
                    CollisionMapID = table.Column<int>(nullable: true),
                    ImagePath = table.Column<string>(nullable: true),
                    MapLayerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tiles_TileBases_BasedOnID",
                        column: x => x.BasedOnID,
                        principalTable: "TileBases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tiles_CollisionMaps_CollisionMapID",
                        column: x => x.CollisionMapID,
                        principalTable: "CollisionMaps",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tiles_MapLayers_MapLayerID",
                        column: x => x.MapLayerID,
                        principalTable: "MapLayers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollisionMaps_CanEnterFromID",
                table: "CollisionMaps",
                column: "CanEnterFromID");

            migrationBuilder.CreateIndex(
                name: "IX_CollisionMaps_CanExitToID",
                table: "CollisionMaps",
                column: "CanExitToID");

            migrationBuilder.CreateIndex(
                name: "IX_MapLayers_TileMapID",
                table: "MapLayers",
                column: "TileMapID");

            migrationBuilder.CreateIndex(
                name: "IX_TileBases_CollisionMapID",
                table: "TileBases",
                column: "CollisionMapID");

            migrationBuilder.CreateIndex(
                name: "IX_Tiles_BasedOnID",
                table: "Tiles",
                column: "BasedOnID");

            migrationBuilder.CreateIndex(
                name: "IX_Tiles_CollisionMapID",
                table: "Tiles",
                column: "CollisionMapID");

            migrationBuilder.CreateIndex(
                name: "IX_Tiles_MapLayerID",
                table: "Tiles",
                column: "MapLayerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tiles");

            migrationBuilder.DropTable(
                name: "TileBases");

            migrationBuilder.DropTable(
                name: "MapLayers");

            migrationBuilder.DropTable(
                name: "CollisionMaps");

            migrationBuilder.DropTable(
                name: "TileMaps");

            migrationBuilder.DropTable(
                name: "DirectionMaps");
        }
    }
}
