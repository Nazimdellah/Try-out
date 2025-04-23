using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Models.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Cost = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayersData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Mana = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayersData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchPlayersData_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsPlayerATurn = table.Column<bool>(type: "bit", nullable: false),
                    IsMatchCompleted = table.Column<bool>(type: "bit", nullable: false),
                    WinnerUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserAId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserBId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerDataAId = table.Column<int>(type: "int", nullable: false),
                    PlayerDataBId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_MatchPlayersData_PlayerDataAId",
                        column: x => x.PlayerDataAId,
                        principalTable: "MatchPlayersData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Matches_MatchPlayersData_PlayerDataBId",
                        column: x => x.PlayerDataBId,
                        principalTable: "MatchPlayersData",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayableCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardId = table.Column<int>(type: "int", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Attack = table.Column<int>(type: "int", nullable: false),
                    MatchPlayerDataId = table.Column<int>(type: "int", nullable: true),
                    MatchPlayerDataId1 = table.Column<int>(type: "int", nullable: true),
                    MatchPlayerDataId2 = table.Column<int>(type: "int", nullable: true),
                    MatchPlayerDataId3 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayableCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayableCard_Cards_CardId",
                        column: x => x.CardId,
                        principalTable: "Cards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayableCard_MatchPlayersData_MatchPlayerDataId",
                        column: x => x.MatchPlayerDataId,
                        principalTable: "MatchPlayersData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayableCard_MatchPlayersData_MatchPlayerDataId1",
                        column: x => x.MatchPlayerDataId1,
                        principalTable: "MatchPlayersData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayableCard_MatchPlayersData_MatchPlayerDataId2",
                        column: x => x.MatchPlayerDataId2,
                        principalTable: "MatchPlayersData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PlayableCard_MatchPlayersData_MatchPlayerDataId3",
                        column: x => x.MatchPlayerDataId3,
                        principalTable: "MatchPlayersData",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "11111111-1111-1111-1111-111111111112", null, "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "11111111-1111-1111-1111-111111111111", 0, "ef4633a0-54a8-482a-b484-fc75a40b9121", "admin@admin.com", true, true, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEGxPubq1VgGlwZSG6/tW4L94zP/wkB3sdzwsGIivZCtHmS02FpSo7NmwOcMoSmD05w==", null, false, "0f37b010-a9f5-4e42-a826-279242d647f4", false, "admin@admin.com" },
                    { "User1Id", 0, "c26fdf7b-2c3b-422c-85aa-99db0f7f0c36", null, false, false, null, null, null, null, null, false, "f8d1d2fd-60e3-4fa9-9cd9-b9446a0d2ff4", false, null },
                    { "User2Id", 0, "7530c8a8-e5d7-4399-8696-3e38d00fc1d4", null, false, false, null, null, null, null, null, false, "a9e109d3-9b05-4425-a475-15ae1b31a225", false, null }
                });

            migrationBuilder.InsertData(
                table: "Cards",
                columns: new[] { "Id", "Attack", "Cost", "Health", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 1, 3, 3, 3, "https://i.pinimg.com/originals/a8/16/49/a81649bd4b0f032ce633161c5a076b87.jpg", "Chat Dragon" },
                    { 2, 2, 3, 5, "https://i0.wp.com/thediscerningcat.com/wp-content/uploads/2021/02/tabby-cat-wearing-sunglasses.jpg", "Chat Awesome" },
                    { 3, 2, 1, 1, "https://cdn.wallpapersafari.com/27/53/SZ8PO9.jpg", "Chatton Laser" },
                    { 4, 8, 4, 4, "https://wallpapers.com/images/hd/epic-cat-poster-baavft05ylgta4j8.jpg", "Chat Spacial" },
                    { 5, 7, 5, 7, "https://i.etsystatic.com/6230905/r/il/32aa5a/3474618751/il_fullxfull.3474618751_mfvf.jpg", "Chat Guerrier" },
                    { 6, 4, 2, 2, "https://store.playstation.com/store/api/chihiro/00_09_000/container/AU/en/99/EP2402-CUSA05624_00-ETH0000000002875/0/image?_version=00_09_000&platform=chihiro&bg_color=000000&opacity=100&w=720&h=720", "Chat Laser" },
                    { 7, 6, 4, 3, "https://images.squarespace-cdn.com/content/51b3dc8ee4b051b96ceb10de/1394662654865-JKOZ7ZFF39247VYDTGG9/hilarious-jedi-cats-fight-video-preview.jpg?content-type=image%2Fjpeg", "Jedi Chat" },
                    { 8, 1, 2, 9, "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/c89c9a3c-7848-4bd5-9306-417c97096ae5/dh8sghm-7bebd975-51f2-4728-87bc-fb3cef176af5.jpg/v1/fit/w_750,h_1000,q_70,strp/another_lucifur_blob_by_slugyyycat_dh8sghm-375w-2x.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7ImhlaWdodCI6Ijw9MTAwMCIsInBhdGgiOiJcL2ZcL2M4OWM5YTNjLTc4NDgtNGJkNS05MzA2LTQxN2M5NzA5NmFlNVwvZGg4c2dobS03YmViZDk3NS01MWYyLTQ3MjgtODdiYy1mYjNjZWYxNzZhZjUuanBnIiwid2lkdGgiOiI8PTc1MCJ9XV0sImF1ZCI6WyJ1cm46c2VydmljZTppbWFnZS5vcGVyYXRpb25zIl19.7oGugpkEX4yqfhiOXlo4TfqzatOuHaCu2aEi-Lnw_40", "Blob Chat" },
                    { 9, 5, 2, 1, "https://townsquare.media/site/142/files/2011/08/jedicats.jpg?w=980&q=75", "Jedi Chatton" },
                    { 10, 6, 2, 1, "https://cdn.theatlantic.com/thumbor/fOZjgqHH0RmXA1A5ek-yDz697W4=/133x0:2091x1020/1200x625/media/img/mt/2015/12/RTRD62Q/original.jpg", "Chat Furtif" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "11111111-1111-1111-1111-111111111112", "11111111-1111-1111-1111-111111111111" });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Test player 1", "User1Id" },
                    { 2, "Test player 2", "User2Id" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerDataAId",
                table: "Matches",
                column: "PlayerDataAId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_PlayerDataBId",
                table: "Matches",
                column: "PlayerDataBId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayersData_PlayerId",
                table: "MatchPlayersData",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayableCard_CardId",
                table: "PlayableCard",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayableCard_MatchPlayerDataId",
                table: "PlayableCard",
                column: "MatchPlayerDataId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayableCard_MatchPlayerDataId1",
                table: "PlayableCard",
                column: "MatchPlayerDataId1");

            migrationBuilder.CreateIndex(
                name: "IX_PlayableCard_MatchPlayerDataId2",
                table: "PlayableCard",
                column: "MatchPlayerDataId2");

            migrationBuilder.CreateIndex(
                name: "IX_PlayableCard_MatchPlayerDataId3",
                table: "PlayableCard",
                column: "MatchPlayerDataId3");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "PlayableCard");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "MatchPlayersData");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
