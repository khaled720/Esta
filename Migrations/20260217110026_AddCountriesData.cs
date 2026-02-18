using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ESTA.Migrations
{
    public partial class AddCountriesData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1, "AF", "أفغانستان", "Afghanistan" },
                    { 2, "AL", "ألبانيا", "Albania" },
                    { 3, "AX", "جزر آلاند", "Aland Islands" },
                    { 4, "DZ", "الجزائر", "Algeria" },
                    { 5, "AS", "ساموا-الأمريكي", "American Samoa" },
                    { 6, "AD", "أندورا", "Andorra" },
                    { 7, "AO", "أنغولا", "Angola" },
                    { 8, "AI", "أنغويلا", "Anguilla" },
                    { 9, "AQ", "أنتاركتيكا", "Antarctica" },
                    { 10, "AG", "أنتيغوا وبربودا", "Antigua and Barbuda" },
                    { 11, "AR", "الأرجنتين", "Argentina" },
                    { 12, "AM", "أرمينيا", "Armenia" },
                    { 13, "AW", "أروبه", "Aruba" },
                    { 14, "AU", "أستراليا", "Australia" },
                    { 15, "AT", "النمسا", "Austria" },
                    { 16, "AZ", "أذربيجان", "Azerbaijan" },
                    { 17, "BS", "الباهاماس", "Bahamas" },
                    { 18, "BH", "البحرين", "Bahrain" },
                    { 19, "BD", "بنغلاديش", "Bangladesh" },
                    { 20, "BB", "بربادوس", "Barbados" },
                    { 21, "BY", "روسيا البيضاء", "Belarus" },
                    { 22, "BE", "بلجيكا", "Belgium" },
                    { 23, "BZ", "بيليز", "Belize" },
                    { 24, "BJ", "بنين", "Benin" },
                    { 25, "BL", "سان بارتيلمي", "Saint Barthelemy" },
                    { 26, "BM", "جزر برمودا", "Bermuda" },
                    { 27, "BT", "بوتان", "Bhutan" },
                    { 28, "BO", "بوليفيا", "Bolivia" },
                    { 29, "BA", "البوسنة و الهرسك", "Bosnia and Herzegovina" },
                    { 30, "BW", "بوتسوانا", "Botswana" },
                    { 31, "BV", "جزيرة بوفيه", "Bouvet Island" },
                    { 32, "BR", "البرازيل", "Brazil" },
                    { 33, "IO", "إقليم المحيط الهندي البريطاني", "British Indian Ocean Territory" },
                    { 34, "BN", "بروني", "Brunei Darussalam" },
                    { 35, "BG", "بلغاريا", "Bulgaria" },
                    { 36, "BF", "بوركينا فاسو", "Burkina Faso" },
                    { 37, "BI", "بوروندي", "Burundi" },
                    { 38, "KH", "كمبوديا", "Cambodia" },
                    { 39, "CM", "كاميرون", "Cameroon" },
                    { 40, "CA", "كندا", "Canada" },
                    { 41, "CV", "الرأس الأخضر", "Cape Verde" },
                    { 42, "KY", "جزر كايمان", "Cayman Islands" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 43, "CF", "جمهورية أفريقيا الوسطى", "Central African Republic" },
                    { 44, "TD", "تشاد", "Chad" },
                    { 45, "CL", "شيلي", "Chile" },
                    { 46, "CN", "الصين", "China" },
                    { 47, "CX", "جزيرة عيد الميلاد", "Christmas Island" },
                    { 48, "CC", "جزر كوكوس", "Cocos (Keeling) Islands" },
                    { 49, "CO", "كولومبيا", "Colombia" },
                    { 50, "KM", "جزر القمر", "Comoros" },
                    { 51, "CG", "الكونغو", "Congo" },
                    { 52, "CK", "جزر كوك", "Cook Islands" },
                    { 53, "CR", "كوستاريكا", "Costa Rica" },
                    { 54, "HR", "كرواتيا", "Croatia" },
                    { 55, "CU", "كوبا", "Cuba" },
                    { 56, "CY", "قبرص", "Cyprus" },
                    { 57, "CW", "كوراساو", "Curaçao" },
                    { 58, "CZ", "الجمهورية التشيكية", "Czech Republic" },
                    { 59, "DK", "الدانمارك", "Denmark" },
                    { 60, "DJ", "جيبوتي", "Djibouti" },
                    { 61, "DM", "دومينيكا", "Dominica" },
                    { 62, "DO", "الجمهورية الدومينيكية", "Dominican Republic" },
                    { 63, "EC", "إكوادور", "Ecuador" },
                    { 64, "EG", "مصر", "Egypt" },
                    { 65, "SV", "إلسلفادور", "El Salvador" },
                    { 66, "GQ", "غينيا الاستوائي", "Equatorial Guinea" },
                    { 67, "ER", "إريتريا", "Eritrea" },
                    { 68, "EE", "استونيا", "Estonia" },
                    { 69, "ET", "أثيوبيا", "Ethiopia" },
                    { 70, "FK", "جزر فوكلاند", "Falkland Islands (Malvinas)" },
                    { 71, "FO", "جزر فارو", "Faroe Islands" },
                    { 72, "FJ", "فيجي", "Fiji" },
                    { 73, "FI", "فنلندا", "Finland" },
                    { 74, "FR", "فرنسا", "France" },
                    { 75, "GF", "غويانا الفرنسية", "French Guiana" },
                    { 76, "PF", "بولينيزيا الفرنسية", "French Polynesia" },
                    { 77, "TF", "أراض فرنسية جنوبية وأنتارتيكية", "French Southern and Antarctic Lands" },
                    { 78, "GA", "الغابون", "Gabon" },
                    { 79, "GM", "غامبيا", "Gambia" },
                    { 80, "GE", "جيورجيا", "Georgia" },
                    { 81, "DE", "ألمانيا", "Germany" },
                    { 82, "GH", "غانا", "Ghana" },
                    { 83, "GI", "جبل طارق", "Gibraltar" },
                    { 84, "GG", "غيرنزي", "Guernsey" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 85, "GR", "اليونان", "Greece" },
                    { 86, "GL", "جرينلاند", "Greenland" },
                    { 87, "GD", "غرينادا", "Grenada" },
                    { 88, "GP", "جزر جوادلوب", "Guadeloupe" },
                    { 89, "GU", "جوام", "Guam" },
                    { 90, "GT", "غواتيمال", "Guatemala" },
                    { 91, "GN", "غينيا", "Guinea" },
                    { 92, "GW", "غينيا-بيساو", "Guinea-Bissau" },
                    { 93, "GY", "غيانا", "Guyana" },
                    { 94, "HT", "هايتي", "Haiti" },
                    { 95, "HM", "جزيرة هيرد وجزر ماكدونالد", "Heard and Mc Donald Islands" },
                    { 96, "HN", "هندوراس", "Honduras" },
                    { 97, "HK", "هونغ كونغ", "Hong Kong" },
                    { 98, "HU", "المجر", "Hungary" },
                    { 99, "IS", "آيسلندا", "Iceland" },
                    { 100, "IN", "الهند", "India" },
                    { 101, "IM", "جزيرة مان", "Isle of Man" },
                    { 102, "ID", "أندونيسيا", "Indonesia" },
                    { 103, "IR", "إيران", "Iran" },
                    { 104, "IQ", "العراق", "Iraq" },
                    { 105, "IE", "إيرلندا", "Ireland" },
                    { 107, "IT", "إيطاليا", "Italy" },
                    { 108, "CI", "ساحل العاج", "Ivory Coast" },
                    { 109, "JE", "جيرزي", "Jersey" },
                    { 110, "JM", "جمايكا", "Jamaica" },
                    { 111, "JP", "اليابان", "Japan" },
                    { 112, "JO", "الأردن", "Jordan" },
                    { 113, "KZ", "كازاخستان", "Kazakhstan" },
                    { 114, "KE", "كينيا", "Kenya" },
                    { 115, "KI", "كيريباتي", "Kiribati" },
                    { 116, "KP", "كوريا الشمالية", "Korea(North Korea)" },
                    { 117, "KR", "كوريا الجنوبية", "Korea(South Korea)" },
                    { 118, "XK", "كوسوفو", "Kosovo" },
                    { 119, "KW", "الكويت", "Kuwait" },
                    { 120, "KG", "قيرغيزستان", "Kyrgyzstan" },
                    { 121, "LA", "لاوس", "Lao PDR" },
                    { 122, "LV", "لاتفيا", "Latvia" },
                    { 123, "LB", "لبنان", "Lebanon" },
                    { 124, "LS", "ليسوتو", "Lesotho" },
                    { 125, "LR", "ليبيريا", "Liberia" },
                    { 126, "LY", "ليبيا", "Libya" },
                    { 127, "LI", "ليختنشتين", "Liechtenstein" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 128, "LT", "لتوانيا", "Lithuania" },
                    { 129, "LU", "لوكسمبورغ", "Luxembourg" },
                    { 130, "LK", "سريلانكا", "Sri Lanka" },
                    { 131, "MO", "ماكاو", "Macau" },
                    { 132, "MK", "مقدونيا", "Macedonia" },
                    { 133, "MG", "مدغشقر", "Madagascar" },
                    { 134, "MW", "مالاوي", "Malawi" },
                    { 135, "MY", "ماليزيا", "Malaysia" },
                    { 136, "MV", "المالديف", "Maldives" },
                    { 137, "ML", "مالي", "Mali" },
                    { 138, "MT", "مالطا", "Malta" },
                    { 139, "MH", "جزر مارشال", "Marshall Islands" },
                    { 140, "MQ", "مارتينيك", "Martinique" },
                    { 141, "MR", "موريتانيا", "Mauritania" },
                    { 142, "MU", "موريشيوس", "Mauritius" },
                    { 143, "YT", "مايوت", "Mayotte" },
                    { 144, "MX", "المكسيك", "Mexico" },
                    { 145, "FM", "مايكرونيزيا", "Micronesia" },
                    { 146, "MD", "مولدافيا", "Moldova" },
                    { 147, "MC", "موناكو", "Monaco" },
                    { 148, "MN", "منغوليا", "Mongolia" },
                    { 149, "ME", "الجبل الأسود", "Montenegro" },
                    { 150, "MS", "مونتسيرات", "Montserrat" },
                    { 151, "MA", "المغرب", "Morocco" },
                    { 152, "MZ", "موزمبيق", "Mozambique" },
                    { 153, "MM", "ميانمار", "Myanmar" },
                    { 154, "NA", "ناميبيا", "Namibia" },
                    { 155, "NR", "نورو", "Nauru" },
                    { 156, "NP", "نيبال", "Nepal" },
                    { 157, "NL", "هولندا", "Netherlands" },
                    { 158, "AN", "جزر الأنتيل الهولندي", "Netherlands Antilles" },
                    { 159, "NC", "كاليدونيا الجديدة", "New Caledonia" },
                    { 160, "NZ", "نيوزيلندا", "New Zealand" },
                    { 161, "NI", "نيكاراجوا", "Nicaragua" },
                    { 162, "NE", "النيجر", "Niger" },
                    { 163, "NG", "نيجيريا", "Nigeria" },
                    { 164, "NU", "ني", "Niue" },
                    { 165, "NF", "جزيرة نورفولك", "Norfolk Island" },
                    { 166, "MP", "جزر ماريانا الشمالية", "Northern Mariana Islands" },
                    { 167, "NO", "النرويج", "Norway" },
                    { 168, "OM", "عمان", "Oman" },
                    { 169, "PK", "باكستان", "Pakistan" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 170, "PW", "بالاو", "Palau" },
                    { 171, "PS", "فلسطين", "Palestine" },
                    { 172, "PA", "بنما", "Panama" },
                    { 173, "PG", "بابوا غينيا الجديدة", "Papua New Guinea" },
                    { 174, "PY", "باراغواي", "Paraguay" },
                    { 175, "PE", "بيرو", "Peru" },
                    { 176, "PH", "الفليبين", "Philippines" },
                    { 177, "PN", "بيتكيرن", "Pitcairn" },
                    { 178, "PL", "بولندا", "Poland" },
                    { 179, "PT", "البرتغال", "Portugal" },
                    { 180, "PR", "بورتو ريكو", "Puerto Rico" },
                    { 181, "QA", "قطر", "Qatar" },
                    { 182, "RE", "ريونيون", "Reunion Island" },
                    { 183, "RO", "رومانيا", "Romania" },
                    { 184, "RU", "روسيا", "Russian" },
                    { 185, "RW", "رواندا", "Rwanda" },
                    { 186, "KN", "سانت كيتس ونيفس,", "Saint Kitts and Nevis" },
                    { 187, "MF", "ساينت مارتن فرنسي", "Saint Martin (French part)" },
                    { 188, "SX", "ساينت مارتن هولندي", "Sint Maarten (Dutch part)" },
                    { 189, "LC", "سان بيير وميكلون", "Saint Pierre and Miquelon" },
                    { 190, "VC", "سانت فنسنت وجزر غرينادين", "Saint Vincent and the Grenadines" },
                    { 191, "WS", "ساموا", "Samoa" },
                    { 192, "SM", "سان مارينو", "San Marino" },
                    { 193, "ST", "ساو تومي وبرينسيبي", "Sao Tome and Principe" },
                    { 194, "SA", "المملكة العربية السعودية", "Saudi Arabia" },
                    { 195, "SN", "السنغال", "Senegal" },
                    { 196, "RS", "صربيا", "Serbia" },
                    { 197, "SC", "سيشيل", "Seychelles" },
                    { 198, "SL", "سيراليون", "Sierra Leone" },
                    { 199, "SG", "سنغافورة", "Singapore" },
                    { 200, "SK", "سلوفاكيا", "Slovakia" },
                    { 201, "SI", "سلوفينيا", "Slovenia" },
                    { 202, "SB", "جزر سليمان", "Solomon Islands" },
                    { 203, "SO", "الصومال", "Somalia" },
                    { 204, "ZA", "جنوب أفريقيا", "South Africa" },
                    { 205, "GS", "المنطقة القطبية الجنوبية", "South Georgia and the South Sandwich" },
                    { 206, "SS", "السودان الجنوبي", "South Sudan" },
                    { 207, "ES", "إسبانيا", "Spain" },
                    { 208, "SH", "سانت هيلانة", "Saint Helena" },
                    { 209, "SD", "السودان", "Sudan" },
                    { 210, "SR", "سورينام", "Suriname" },
                    { 211, "SJ", "سفالبارد ويان ماين", "Svalbard and Jan Mayen" }
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Code", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 212, "SZ", "سوازيلند", "Swaziland" },
                    { 213, "SE", "السويد", "Sweden" },
                    { 214, "CH", "سويسرا", "Switzerland" },
                    { 215, "SY", "سوريا", "Syria" },
                    { 216, "TW", "تايوان", "Taiwan" },
                    { 217, "TJ", "طاجيكستان", "Tajikistan" },
                    { 218, "TZ", "تنزانيا", "Tanzania" },
                    { 219, "TH", "تايلندا", "Thailand" },
                    { 220, "TL", "تيمور الشرقية", "Timor-Leste" },
                    { 221, "TG", "توغو", "Togo" },
                    { 222, "TK", "توكيلاو", "Tokelau" },
                    { 223, "TO", "تونغا", "Tonga" },
                    { 224, "TT", "ترينيداد وتوباغو", "Trinidad and Tobago" },
                    { 225, "TN", "تونس", "Tunisia" },
                    { 226, "TR", "تركيا", "Turkey" },
                    { 227, "TM", "تركمانستان", "Turkmenistan" },
                    { 228, "TC", "جزر توركس وكايكوس", "Turks and Caicos Islands" },
                    { 229, "TV", "توفالو", "Tuvalu" },
                    { 230, "UG", "أوغندا", "Uganda" },
                    { 231, "UA", "أوكرانيا", "Ukraine" },
                    { 232, "AE", "الإمارات العربية المتحدة", "United Arab Emirates" },
                    { 233, "GB", "المملكة المتحدة", "United Kingdom" },
                    { 234, "US", "الولايات المتحدة", "United States" },
                    { 235, "UM", "قائمة الولايات والمناطق الأمريكية", "US Minor Outlying Islands" },
                    { 236, "UY", "أورغواي", "Uruguay" },
                    { 237, "UZ", "أوزباكستان", "Uzbekistan" },
                    { 238, "VU", "فانواتو", "Vanuatu" },
                    { 239, "VE", "فنزويلا", "Venezuela" },
                    { 240, "VN", "فيتنام", "Vietnam" },
                    { 241, "VI", "الجزر العذراء الأمريكي", "Virgin Islands (U.S.)" },
                    { 242, "VA", "فنزويلا", "Vatican City" },
                    { 243, "WF", "والس وفوتونا", "Wallis and Futuna Islands" },
                    { 244, "EH", "الصحراء الغربية", "Western Sahara" },
                    { 245, "YE", "اليمن", "Yemen" },
                    { 246, "ZM", "زامبيا", "Zambia" },
                    { 247, "ZW", "زمبابوي", "Zimbabwe" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 75);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 94);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 95);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 96);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 97);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 98);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 99);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 100);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 112);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 113);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 114);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 115);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 116);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 117);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 118);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 119);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 120);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 121);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 122);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 123);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 124);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 125);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 126);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 127);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 128);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 129);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 130);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 131);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 132);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 133);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 134);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 135);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 136);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 137);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 138);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 139);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 140);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 141);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 142);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 143);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 144);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 145);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 146);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 147);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 148);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 149);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 150);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 151);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 152);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 153);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 154);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 155);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 156);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 157);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 158);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 159);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 160);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 161);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 162);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 163);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 164);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 165);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 166);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 167);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 168);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 169);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 170);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 171);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 172);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 173);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 174);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 175);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 176);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 177);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 178);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 179);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 180);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 181);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 182);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 183);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 184);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 185);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 186);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 187);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 188);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 189);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 190);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 191);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 192);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 193);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 194);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 195);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 196);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 197);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 198);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 199);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 203);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 204);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 205);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 206);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 207);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 208);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 209);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 210);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 211);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 212);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 213);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 214);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 215);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 216);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 217);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 218);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 219);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 220);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 221);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 223);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 224);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 225);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 226);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 227);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 228);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 229);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 230);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 231);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 232);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 233);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 234);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 235);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 236);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 237);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 238);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 239);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 240);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 241);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 242);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 243);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 244);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 245);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 246);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 247);
        }
    }
}
