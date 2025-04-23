using System;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using Super_Cartes_Infinies.Models;

namespace Super_Cartes_Infinies.Data
{
    public class Seed
    {
        public Seed() { }

        public static Card[] SeedCards()
        {
            return new Card[] {
                new Card
                {
                    Id = 1,
                    Name = "Chat Dragon",
                    Attack = 3,
                    Health = 3,
                    Cost = 3,
                    ImageUrl = "https://i.pinimg.com/originals/a8/16/49/a81649bd4b0f032ce633161c5a076b87.jpg"
                }, new Card
                {
                    Id = 2,
                    Name = "Chat Awesome",
                    Attack = 2,
                    Health = 5,
                    Cost = 3,
                    ImageUrl = "https://i0.wp.com/thediscerningcat.com/wp-content/uploads/2021/02/tabby-cat-wearing-sunglasses.jpg"
                }, new Card
                {
                    Id = 3,
                    Name = "Chatton Laser",
                    Attack = 2,
                    Health = 1,
                    Cost = 1,
                    ImageUrl = "https://cdn.wallpapersafari.com/27/53/SZ8PO9.jpg"
                }, new Card
                {
                    Id = 4,
                    Name = "Chat Spacial",
                    Attack = 8,
                    Health = 4,
                    Cost = 4,
                    ImageUrl = "https://wallpapers.com/images/hd/epic-cat-poster-baavft05ylgta4j8.jpg"
                }, new Card
                {
                    Id = 5,
                    Name = "Chat Guerrier",
                    Attack = 7,
                    Health = 7,
                    Cost = 5,
                    ImageUrl = "https://i.etsystatic.com/6230905/r/il/32aa5a/3474618751/il_fullxfull.3474618751_mfvf.jpg"
                }, new Card
                {
                    Id = 6,
                    Name = "Chat Laser",
                    Attack = 4,
                    Health = 2,
                    Cost = 2,
                    ImageUrl = "https://store.playstation.com/store/api/chihiro/00_09_000/container/AU/en/99/EP2402-CUSA05624_00-ETH0000000002875/0/image?_version=00_09_000&platform=chihiro&bg_color=000000&opacity=100&w=720&h=720"
                }, new Card
                {
                    Id = 7,
                    Name = "Jedi Chat",
                    Attack = 6,
                    Health = 3,
                    Cost = 4,
                    ImageUrl = "https://images.squarespace-cdn.com/content/51b3dc8ee4b051b96ceb10de/1394662654865-JKOZ7ZFF39247VYDTGG9/hilarious-jedi-cats-fight-video-preview.jpg?content-type=image%2Fjpeg"
                }, new Card
                {
                    Id = 8,
                    Name = "Blob Chat",
                    Attack = 1,
                    Health = 9,
                    Cost = 2,
                    ImageUrl = "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/c89c9a3c-7848-4bd5-9306-417c97096ae5/dh8sghm-7bebd975-51f2-4728-87bc-fb3cef176af5.jpg/v1/fit/w_750,h_1000,q_70,strp/another_lucifur_blob_by_slugyyycat_dh8sghm-375w-2x.jpg?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOjdlMGQxODg5ODIyNjQzNzNhNWYwZDQxNWVhMGQyNmUwIiwiaXNzIjoidXJuOmFwcDo3ZTBkMTg4OTgyMjY0MzczYTVmMGQ0MTVlYTBkMjZlMCIsIm9iaiI6W1t7ImhlaWdodCI6Ijw9MTAwMCIsInBhdGgiOiJcL2ZcL2M4OWM5YTNjLTc4NDgtNGJkNS05MzA2LTQxN2M5NzA5NmFlNVwvZGg4c2dobS03YmViZDk3NS01MWYyLTQ3MjgtODdiYy1mYjNjZWYxNzZhZjUuanBnIiwid2lkdGgiOiI8PTc1MCJ9XV0sImF1ZCI6WyJ1cm46c2VydmljZTppbWFnZS5vcGVyYXRpb25zIl19.7oGugpkEX4yqfhiOXlo4TfqzatOuHaCu2aEi-Lnw_40"
                }, new Card
                {
                    Id = 9,
                    Name = "Jedi Chatton",
                    Attack = 5,
                    Health = 1,
                    Cost = 2,
                    ImageUrl = "https://townsquare.media/site/142/files/2011/08/jedicats.jpg?w=980&q=75"
                }, new Card
                {
                    Id = 10,
                    Name = "Chat Furtif",
                    Attack = 6,
                    Health = 1,
                    Cost = 2,
                    ImageUrl = "https://cdn.theatlantic.com/thumbor/fOZjgqHH0RmXA1A5ek-yDz697W4=/133x0:2091x1020/1200x625/media/img/mt/2015/12/RTRD62Q/original.jpg"
                }
            };
        }

        public static IdentityUser[] SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();
            IdentityUser admin = new IdentityUser
            {
                Id = "11111111-1111-1111-1111-111111111111",
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                // La comparaison d'identity se fait avec les versions normalisés
                NormalizedEmail = "ADMIN@ADMIN.COM",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                // On encrypte le mot de passe
                PasswordHash = hasher.HashPassword(null, "Passw0rd!"),
                LockoutEnabled = true
            };

            return new IdentityUser[] { admin };
        }

        public static IdentityRole[] SeedRoles()
        {
            IdentityRole adminRole = new IdentityRole
            {
                Id = "11111111-1111-1111-1111-111111111112",
                Name = ApplicationDbContext.ADMIN_ROLE,
                NormalizedName = ApplicationDbContext.ADMIN_ROLE.ToUpper()
            };

            return new IdentityRole[] { adminRole };
        }

        public static IdentityUserRole<string>[] SeedUserRoles()
        {
            IdentityUserRole<string> userAdmin = new IdentityUserRole<string>
            {
                RoleId = "11111111-1111-1111-1111-111111111112",
                UserId = "11111111-1111-1111-1111-111111111111"
            };
            return new IdentityUserRole<string>[] { userAdmin };
        }

        public static IdentityUser[] SeedTestUsers()
        {
            return new IdentityUser[] {
                new IdentityUser()
                {
                    Id = "User1Id"
                },
                new IdentityUser
                {
                Id = "User2Id"
                }
            };
        }

        public static Player[] SeedTestPlayers()
        {
            return new Player[] {
                new Player
                {
                    Id = 1,
                    Name = "Test player 1",
                    UserId = "User1Id"

                },
                new Player
                {
                    Id = 2,
                    Name = "Test player 2",
                    UserId = "User2Id"
                }
            };
        }
    }
}

