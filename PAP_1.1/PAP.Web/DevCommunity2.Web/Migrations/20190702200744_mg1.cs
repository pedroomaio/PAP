using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevCommunity2.Web.Migrations
{
    public partial class mg1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    NickName = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(nullable: true, defaultValue: "DefaultUserPhoto.png")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameEvent = table.Column<string>(maxLength: 20, nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateEvent = table.Column<DateTime>(nullable: false),
                    TypeOfEvent = table.Column<string>(maxLength: 20, nullable: true),
                    LocationWhat3words = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    PhotoUrl = table.Column<string>(maxLength: 500, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Stars = table.Column<int>(nullable: false, defaultValue: 3),
                    IsEnabled = table.Column<bool>(nullable: false, defaultValue: true),
                    CreatedByUserID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                name: "AccountPublish",
                columns: table => new
                {
                    AccountPublishId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<Guid>(nullable: false),
                    DatePublish = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountPublish", x => x.AccountPublishId);
                    table.ForeignKey(
                        name: "FK_AccountPublish_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRelationship",
                columns: table => new
                {
                    AccountRelationshipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SenderAccountId = table.Column<Guid>(nullable: false),
                    ReceiverAccountId = table.Column<Guid>(nullable: false),
                    Isfriend = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRelationship", x => x.AccountRelationshipId);
                    table.ForeignKey(
                        name: "FK_AccountRelationship_AspNetUsers_ReceiverAccountId",
                        column: x => x.ReceiverAccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountRelationship_AspNetUsers_SenderAccountId",
                        column: x => x.SenderAccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "AccountNotifications",
                columns: table => new
                {
                    AccountNotificationsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SenderNotificationAccountId = table.Column<Guid>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: true),
                    Text = table.Column<string>(maxLength: 50, nullable: true),
                    NotificationsDate = table.Column<DateTime>(type: "date", nullable: false),
                    RedirectUrl = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false),
                    Seen = table.Column<bool>(nullable: false),
                    ReceiverNotificationAccountId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNotifications", x => x.AccountNotificationsId);
                    table.ForeignKey(
                        name: "FK_AccountNotifications_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountNotifications_AspNetUsers_ReceiverNotificationAccountId",
                        column: x => x.ReceiverNotificationAccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountNotifications_AspNetUsers_SenderNotificationAccountId",
                        column: x => x.SenderNotificationAccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountOnEvent",
                columns: table => new
                {
                    AccountOnEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountId = table.Column<Guid>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountOnEvent", x => x.AccountOnEventId);
                    table.ForeignKey(
                        name: "FK_AccountOnEvent_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountOnEvent_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublishEvent",
                columns: table => new
                {
                    PublishEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventId = table.Column<int>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    PublishDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishEvent", x => x.PublishEventId);
                    table.ForeignKey(
                        name: "FK_PublishEvent_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublishEvent_Event_EventId",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContentPublishAccount",
                columns: table => new
                {
                    ContentPublishAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TextContent = table.Column<string>(nullable: true),
                    GithubFile = table.Column<string>(maxLength: 500, nullable: true),
                    Tags = table.Column<string>(nullable: true),
                    AccountPublishId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentPublishAccount", x => x.ContentPublishAccountId);
                    table.ForeignKey(
                        name: "FK_ContentPublishAccount_AccountPublish_AccountPublishId",
                        column: x => x.AccountPublishId,
                        principalTable: "AccountPublish",
                        principalColumn: "AccountPublishId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedBackContentAccount",
                columns: table => new
                {
                    FeedBackContentAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountPublishId = table.Column<int>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Stars = table.Column<int>(nullable: false, defaultValue: 3)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBackContentAccount", x => x.FeedBackContentAccountId);
                    table.ForeignKey(
                        name: "FK_FeedBackContentAccount_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedBackContentAccount_AccountPublish_AccountPublishId",
                        column: x => x.AccountPublishId,
                        principalTable: "AccountPublish",
                        principalColumn: "AccountPublishId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContentPublishEvent",
                columns: table => new
                {
                    ContentPublishEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublishEventId = table.Column<int>(nullable: false),
                    TextContent = table.Column<string>(nullable: true),
                    GithubFile = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentPublishEvent", x => x.ContentPublishEventId);
                    table.ForeignKey(
                        name: "FK_ContentPublishEvent_PublishEvent_PublishEventId",
                        column: x => x.PublishEventId,
                        principalTable: "PublishEvent",
                        principalColumn: "PublishEventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeedBackContentEvent",
                columns: table => new
                {
                    IdFeedBackContent = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EventPublishId = table.Column<int>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    Stars = table.Column<int>(nullable: false, defaultValue: 3),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedBackContentEvent", x => x.IdFeedBackContent);
                    table.ForeignKey(
                        name: "FK_FeedBackContentEvent_AspNetUsers_AccountId",
                        column: x => x.AccountId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FeedBackContentEvent_PublishEvent_EventPublishId",
                        column: x => x.EventPublishId,
                        principalTable: "PublishEvent",
                        principalColumn: "PublishEventId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhotoContentPublishAccount",
                columns: table => new
                {
                    PhotoContentPublishAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentPublishAccountId = table.Column<int>(nullable: false),
                    PhotoURl = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoContentPublishAccount", x => x.PhotoContentPublishAccountId);
                    table.ForeignKey(
                        name: "FK_PhotoContentPublishAccount_ContentPublishAccount_ContentPublishAccountId",
                        column: x => x.ContentPublishAccountId,
                        principalTable: "ContentPublishAccount",
                        principalColumn: "ContentPublishAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoContentPublishAccount",
                columns: table => new
                {
                    VideoContentPublishAccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentPublishAccountId = table.Column<int>(nullable: false),
                    VideoURl = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoContentPublishAccount", x => x.VideoContentPublishAccountId);
                    table.ForeignKey(
                        name: "FK_VideoContentPublishAccount_ContentPublishAccount_ContentPublishAccountId",
                        column: x => x.ContentPublishAccountId,
                        principalTable: "ContentPublishAccount",
                        principalColumn: "ContentPublishAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotoContentPublishEvent",
                columns: table => new
                {
                    PhotoContentPublishEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentPublishEventId = table.Column<int>(nullable: false),
                    PhotoURl = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoContentPublishEvent", x => x.PhotoContentPublishEventId);
                    table.ForeignKey(
                        name: "FK_PhotoContentPublishEvent_ContentPublishEvent_ContentPublishEventId",
                        column: x => x.ContentPublishEventId,
                        principalTable: "ContentPublishEvent",
                        principalColumn: "ContentPublishEventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoContentPublishEvent",
                columns: table => new
                {
                    VideoContentPublishEventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentPublishEventId = table.Column<int>(nullable: false),
                    VideoUrl = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoContentPublishEvent", x => x.VideoContentPublishEventId);
                    table.ForeignKey(
                        name: "FK_VideoContentPublishEvent_ContentPublishEvent_ContentPublishEventId",
                        column: x => x.ContentPublishEventId,
                        principalTable: "ContentPublishEvent",
                        principalColumn: "ContentPublishEventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountNotifications_EventId",
                table: "AccountNotifications",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountNotifications_ReceiverNotificationAccountId",
                table: "AccountNotifications",
                column: "ReceiverNotificationAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountNotifications_SenderNotificationAccountId_ReceiverNotificationAccountId",
                table: "AccountNotifications",
                columns: new[] { "SenderNotificationAccountId", "ReceiverNotificationAccountId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountOnEvent_AccountId",
                table: "AccountOnEvent",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountOnEvent_EventId",
                table: "AccountOnEvent",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountPublish_AccountId",
                table: "AccountPublish",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRelationship_ReceiverAccountId",
                table: "AccountRelationship",
                column: "ReceiverAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRelationship_SenderAccountId_ReceiverAccountId",
                table: "AccountRelationship",
                columns: new[] { "SenderAccountId", "ReceiverAccountId" },
                unique: true);

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
                name: "IX_ContentPublishAccount_AccountPublishId",
                table: "ContentPublishAccount",
                column: "AccountPublishId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentPublishEvent_PublishEventId",
                table: "ContentPublishEvent",
                column: "PublishEventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedBackContentAccount_AccountId",
                table: "FeedBackContentAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBackContentAccount_AccountPublishId",
                table: "FeedBackContentAccount",
                column: "AccountPublishId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBackContentEvent_AccountId",
                table: "FeedBackContentEvent",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedBackContentEvent_EventPublishId",
                table: "FeedBackContentEvent",
                column: "EventPublishId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoContentPublishAccount_ContentPublishAccountId",
                table: "PhotoContentPublishAccount",
                column: "ContentPublishAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotoContentPublishEvent_ContentPublishEventId",
                table: "PhotoContentPublishEvent",
                column: "ContentPublishEventId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishEvent_AccountId",
                table: "PublishEvent",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishEvent_EventId",
                table: "PublishEvent",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoContentPublishAccount_ContentPublishAccountId",
                table: "VideoContentPublishAccount",
                column: "ContentPublishAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoContentPublishEvent_ContentPublishEventId",
                table: "VideoContentPublishEvent",
                column: "ContentPublishEventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountNotifications");

            migrationBuilder.DropTable(
                name: "AccountOnEvent");

            migrationBuilder.DropTable(
                name: "AccountRelationship");

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
                name: "FeedBackContentAccount");

            migrationBuilder.DropTable(
                name: "FeedBackContentEvent");

            migrationBuilder.DropTable(
                name: "PhotoContentPublishAccount");

            migrationBuilder.DropTable(
                name: "PhotoContentPublishEvent");

            migrationBuilder.DropTable(
                name: "VideoContentPublishAccount");

            migrationBuilder.DropTable(
                name: "VideoContentPublishEvent");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ContentPublishAccount");

            migrationBuilder.DropTable(
                name: "ContentPublishEvent");

            migrationBuilder.DropTable(
                name: "AccountPublish");

            migrationBuilder.DropTable(
                name: "PublishEvent");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Event");
        }
    }
}
