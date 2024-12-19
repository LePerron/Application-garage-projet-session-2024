using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace ProjetSession.Migrations
{
    /// <inheritdoc />
    public partial class SeedingEntretiensInitiaux : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Entretiens",
                columns: new[] { "Id", "TypeEntretien", "Date", "Etat", "Kilometrage", "Vin" },
                values: new object[,]
                {
                //                          Véhicule 1
                    //  Entretiens moteur
                    { 1, 1, new DateTime(2022, 11, 22), 45, 10295, "1FSDDASDSAS14245" },
                    { 2, 1, new DateTime(2023, 03, 15), 82, 13126, "1FSDDASDSAS14245" },
                    //  Entretiens huile
                    { 11, 2, new DateTime(2022, 05, 12), 34, 9830, "1FSDDASDSAS14245" },
                    { 12, 2, new DateTime(2023, 02, 27), 65, 12322, "1FSDDASDSAS14245" },
                    //  Entretiens transmission
                    { 21, 3, new DateTime(2023, 05, 28), 65, 15625, "1FSDDASDSAS14245" },
                    { 22, 3, new DateTime(2024, 02, 02), 72, 20365, "1FSDDASDSAS14245" },
                    //  Entretiens pneus
                    { 31, 4, new DateTime(2022, 05, 06), 25, 9523, "1FSDDASDSAS14245" },
                    { 32, 4, new DateTime(2023, 08, 17), 100, 16985, "1FSDDASDSAS14245" },

                //                          Véhicule 2
                    //  Entretiens moteur
                    { 3, 1, new DateTime(2022, 01, 18), 89, 98067, "1FAVDASDSAS12345" },
                    { 4, 1, new DateTime(2024, 07, 05), 67, 120560, "1FAVDASDSAS12345" },
                    //  Entretiens huile
                    { 13, 2, new DateTime(2019, 10, 09), 78, 92986, "1FAVDASDSAS12345" },
                    { 14, 2, new DateTime(2024, 06, 14), 90, 119253, "1FAVDASDSAS12345" },
                    //  Entretiens transmission
                    { 23, 3, new DateTime(2019, 01, 16), 80, 90562, "1FAVDASDSAS12345" },
                    { 24, 3, new DateTime(2024, 04, 10), 43, 118318, "1FAVDASDSAS12345" },
                    //  Entretiens pneus
                    { 33, 4, new DateTime(2022, 07, 15), 55, 111536, "1FAVDASDSAS12345" },
                    { 34, 4, new DateTime(2023, 01, 24), 45, 116985, "1FAVDASDSAS12345" },


                //                          Véhicule 3
                    // Entretiens moteur
                    { 5, 1, new DateTime(2022, 03, 25), 100, 9830, "2FAJGASDSAS13246" },
                    { 6, 1, new DateTime(2023, 07, 15), 82, 13126, "2FAJGASDSAS13246" },
                    { 7, 1, new DateTime(2024, 11, 30), 45, 18521, "2FAJGASDSAS13246" },
                    // Entretiens huile
                    { 15, 2, new DateTime(2022, 05, 12), 34, 10295, "2FAJGASDSAS13246" },
                    { 16, 2, new DateTime(2023, 02, 27), 65, 12322, "2FAJGASDSAS13246" },
                    { 17, 2, new DateTime(2024, 08, 15), 62, 16895, "2FAJGASDSAS13246" },
                    // Entretiens transmission
                    { 25, 3, new DateTime(2019, 05, 28), 100, 7563, "2FAJGASDSAS13246" },
                    { 26, 3, new DateTime(2022, 02, 05), 72, 9702, "2FAJGASDSAS13246" },
                    { 27, 3, new DateTime(2023, 10, 02), 60, 15120, "2FAJGASDSAS13246" },
                    // Entretiens pneus
                    { 35, 4, new DateTime(2022, 07, 06), 25, 11520, "2FAJGASDSAS13246" },
                    { 36, 4, new DateTime(2023, 08, 17), 100, 14260, "2FAJGASDSAS13246" },
                    { 37, 4, new DateTime(2024, 09, 23), 65, 17325, "2FAJGASDSAS13246" },


                //                          Véhicule 4
                    //  Entretiens moteur
                    { 8, 1, new DateTime(2023, 01, 20), 50, 111_923, "5CEDFJLIJKG45312" },
                    { 9, 1, new DateTime(2023, 05, 25), 20, 115_698, "5CEDFJLIJKG45312" },
                    { 10, 1, new DateTime(2024, 07, 05), 70, 233_650, "5CEDFJLIJKG45312" },
                    //  Entretiens huile
                    { 18, 2, new DateTime(2023, 08, 25), 55, 135_552, "5CEDFJLIJKG45312" },
                    { 19, 2, new DateTime(2023, 11, 07), 66, 173_472, "5CEDFJLIJKG45312" },
                    { 20, 2, new DateTime(2024, 01, 11), 77, 198_219, "5CEDFJLIJKG45312" },
                    //  Entretiens transmission
                    { 28, 3, new DateTime(2023, 06, 29), 77, 118_236, "5CEDFJLIJKG45312" },
                    { 29, 3, new DateTime(2023, 09, 04), 84, 155_431, "5CEDFJLIJKG45312" },
                    { 30, 3, new DateTime(2024, 11, 20), 100, 278_6521, "5CEDFJLIJKG45312" },
                    //  Entretiens pneus
                    { 38, 4, new DateTime(2023, 02, 28), 80, 113_562, "5CEDFJLIJKG45312" },
                    { 39, 4, new DateTime(2024, 05, 25), 20, 221_475, "5CEDFJLIJKG45312" },
                    { 40, 4, new DateTime(2024, 07, 30), 100, 262_278, "5CEDFJLIJKG45312" },


                //                          Véhicule 5
                    //  Entretiens moteur
                    { 41, 1, new DateTime(2019, 08, 30), 21, 121_159, "9ASRFASFARG32351" },
                    { 42, 1, new DateTime(2023, 09, 10), 80, 288_512, "9ASRFASFARG32351" },
                    //  Entretiens huile
                    { 43, 2, new DateTime(2021, 04, 03), 33, 180_422, "9ASRFASFARG32351" },
                    { 44, 2, new DateTime(2023, 09, 19), 44, 291_371, "9ASRFASFARG32351" },
                    { 45, 2, new DateTime(2024, 11, 30), 95, 332_721, "9ASRFASFARG32351" },
                    //  Entretiens transmission
                    { 46, 3, new DateTime(2020, 03, 23), 63, 160_943, "9ASRFASFARG32351" },
                    { 47, 3, new DateTime(2022, 12, 13), 62, 225_322, "9ASRFASFARG32351" },
                    { 48, 3, new DateTime(2023, 10, 08), 96, 310_122, "9ASRFASFARG32351" },
                    //  Entretiens pneus
                    { 49, 4, new DateTime(2019, 03, 01), 65, 98_212, "9ASRFASFARG32351" },
                    { 50, 4, new DateTime(2022, 09, 10), 75, 201_234, "9ASRFASFARG32351" },
                    { 51, 4, new DateTime(2023, 10, 15), 85, 316_245, "9ASRFASFARG32351" },

                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Entretiens");
        }
    }
}
