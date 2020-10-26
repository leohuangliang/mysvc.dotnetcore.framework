using System;
using System.Collections.Generic;
namespace MySvc.DotNetCore.Framework.Domain.Core.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Country
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string TwoLetterCode { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThreeLetterCode { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string NumericCode { get; private set; }

        private Country(string name, string twoLetterCode, string threeLetterCode, string numericCode)
        {
            Name = name;
            TwoLetterCode = twoLetterCode;
            ThreeLetterCode = threeLetterCode;
            NumericCode = numericCode;
        }

        /// <summary>
        /// 国家字典
        /// </summary>
        public static readonly Dictionary<string, Country> List = new Dictionary<string, Country>()
        {

            { AF,  new Country("Afghanistan", "AF", "AFG", "4")},
            { AX,  new Country("Åland Islands", "AX", "ALA", "248")},
            { AL,  new Country("Albania", "AL", "ALB", "8")},
            { DZ,  new Country("Algeria", "DZ", "DZA", "12")},
            { AS,  new Country("American Samoa", "AS", "ASM", "16")},
            { AD,  new Country("Andorra", "AD", "AND", "20")},
            { AO,  new Country("Angola", "AO", "AGO", "24")},
            { AI,  new Country("Anguilla", "AI", "AIA", "660")},
            { AQ,  new Country("Antarctica", "AQ", "ATA", "10")},
            { AG,  new Country("Antigua and Barbuda", "AG", "ATG", "28")},
            { AR,  new Country("Argentina", "AR", "ARG", "32")},
            { AM,  new Country("Armenia", "AM", "ARM", "51")},
            { AW,  new Country("Aruba", "AW", "ABW", "533")},
            { AU,  new Country("Australia", "AU", "AUS", "36")},
            { AT,  new Country("Austria", "AT", "AUT", "40")},
            { AZ,  new Country("Azerbaijan", "AZ", "AZE", "31")},
            { BS,  new Country("Bahamas", "BS", "BHS", "44")},
            { BH,  new Country("Bahrain", "BH", "BHR", "48")},
            { BD,  new Country("Bangladesh", "BD", "BGD", "50")},
            { BB,  new Country("Barbados", "BB", "BRB", "52")},
            { BY,  new Country("Belarus", "BY", "BLR", "112")},
            { BE,  new Country("Belgium", "BE", "BEL", "56")},
            { BZ,  new Country("Belize", "BZ", "BLZ", "84")},
            { BJ,  new Country("Benin", "BJ", "BEN", "204")},
            { BM,  new Country("Bermuda", "BM", "BMU", "60")},
            { BT,  new Country("Bhutan", "BT", "BTN", "64")},
            { BO,  new Country("Bolivia (Plurinational State of)", "BO", "BOL", "68")},
            { BQ,  new Country("Bonaire, Sint Eustatius and Saba", "BQ", "BES", "535")},
            { BA,  new Country("Bosnia and Herzegovina", "BA", "BIH", "70")},
            { BW,  new Country("Botswana", "BW", "BWA", "72")},
            { BV,  new Country("Bouvet Island", "BV", "BVT", "74")},
            { BR,  new Country("Brazil", "BR", "BRA", "76")},
            { IO,  new Country("British Indian Ocean Territory", "IO", "IOT", "86")},
            { BN,  new Country("Brunei Darussalam", "BN", "BRN", "96")},
            { BG,  new Country("Bulgaria", "BG", "BGR", "100")},
            { BF,  new Country("Burkina Faso", "BF", "BFA", "854")},
            { BI,  new Country("Burundi", "BI", "BDI", "108")},
            { CV,  new Country("Cabo Verde", "CV", "CPV", "132")},
            { KH,  new Country("Cambodia", "KH", "KHM", "116")},
            { CM,  new Country("Cameroon", "CM", "CMR", "120")},
            { CA,  new Country("Canada", "CA", "CAN", "124")},
            { KY,  new Country("Cayman Islands", "KY", "CYM", "136")},
            { CF,  new Country("Central African Republic", "CF", "CAF", "140")},
            { TD,  new Country("Chad", "TD", "TCD", "148")},
            { CL,  new Country("Chile", "CL", "CHL", "152")},
            { CN,  new Country("China", "CN", "CHN", "156")},
            { CX,  new Country("Christmas Island", "CX", "CXR", "162")},
            { CC,  new Country("Cocos (Keeling) Islands", "CC", "CCK", "166")},
            { CO,  new Country("Colombia", "CO", "COL", "170")},
            { KM,  new Country("Comoros", "KM", "COM", "174")},
            { CG,  new Country("Congo", "CG", "COG", "178")},
            { CD,  new Country("Congo, Democratic Republic of the", "CD", "COD", "180")},
            { CK,  new Country("Cook Islands", "CK", "COK", "184")},
            { CR,  new Country("Costa Rica", "CR", "CRI", "188")},
            { CI,  new Country("CÃ´te d'Ivoire", "CI", "CIV", "384")},
            { HR,  new Country("Croatia", "HR", "HRV", "191")},
            { CU,  new Country("Cuba", "CU", "CUB", "192")},
            { CW,  new Country("CuraÃ§ao", "CW", "CUW", "531")},
            { CY,  new Country("Cyprus", "CY", "CYP", "196")},
            { CZ,  new Country("Czechia", "CZ", "CZE", "203")},
            { DK,  new Country("Denmark", "DK", "DNK", "208")},
            { DJ,  new Country("Djibouti", "DJ", "DJI", "262")},
            { DM,  new Country("Dominica", "DM", "DMA", "212")},
            { DO,  new Country("Dominican Republic", "DO", "DOM", "214")},
            { EC,  new Country("Ecuador", "EC", "ECU", "218")},
            { EG,  new Country("Egypt", "EG", "EGY", "818")},
            { SV,  new Country("El Salvador", "SV", "SLV", "222")},
            { GQ,  new Country("Equatorial Guinea", "GQ", "GNQ", "226")},
            { ER,  new Country("Eritrea", "ER", "ERI", "232")},
            { EE,  new Country("Estonia", "EE", "EST", "233")},
            { SZ,  new Country("Eswatini", "SZ", "SWZ", "748")},
            { ET,  new Country("Ethiopia", "ET", "ETH", "231")},
            { FK,  new Country("Falkland Islands (Malvinas)", "FK", "FLK", "238")},
            { FO,  new Country("Faroe Islands", "FO", "FRO", "234")},
            { FJ,  new Country("Fiji", "FJ", "FJI", "242")},
            { FI,  new Country("Finland", "FI", "FIN", "246")},
            { FR,  new Country("France", "FR", "FRA", "250")},
            { GF,  new Country("French Guiana", "GF", "GUF", "254")},
            { PF,  new Country("French Polynesia", "PF", "PYF", "258")},
            { TF,  new Country("French Southern Territories", "TF", "ATF", "260")},
            { GA,  new Country("Gabon", "GA", "GAB", "266")},
            { GM,  new Country("Gambia", "GM", "GMB", "270")},
            { GE,  new Country("Georgia", "GE", "GEO", "268")},
            { DE,  new Country("Germany", "DE", "DEU", "276")},
            { GH,  new Country("Ghana", "GH", "GHA", "288")},
            { GI,  new Country("Gibraltar", "GI", "GIB", "292")},
            { GR,  new Country("Greece", "GR", "GRC", "300")},
            { GL,  new Country("Greenland", "GL", "GRL", "304")},
            { GD,  new Country("Grenada", "GD", "GRD", "308")},
            { GP,  new Country("Guadeloupe", "GP", "GLP", "312")},
            { GU,  new Country("Guam", "GU", "GUM", "316")},
            { GT,  new Country("Guatemala", "GT", "GTM", "320")},
            { GG,  new Country("Guernsey", "GG", "GGY", "831")},
            { GN,  new Country("Guinea", "GN", "GIN", "324")},
            { GW,  new Country("Guinea-Bissau", "GW", "GNB", "624")},
            { GY,  new Country("Guyana", "GY", "GUY", "328")},
            { HT,  new Country("Haiti", "HT", "HTI", "332")},
            { HM,  new Country("Heard Island and McDonald Islands", "HM", "HMD", "334")},
            { VA,  new Country("Holy See", "VA", "VAT", "336")},
            { HN,  new Country("Honduras", "HN", "HND", "340")},
            { HK,  new Country("Hong Kong", "HK", "HKG", "344")},
            { HU,  new Country("Hungary", "HU", "HUN", "348")},
            { IS,  new Country("Iceland", "IS", "ISL", "352")},
            { IN,  new Country("India", "IN", "IND", "356")},
            { ID,  new Country("Indonesia", "ID", "IDN", "360")},
            { IR,  new Country("Iran (Islamic Republic of)", "IR", "IRN", "364")},
            { IQ,  new Country("Iraq", "IQ", "IRQ", "368")},
            { IE,  new Country("Ireland", "IE", "IRL", "372")},
            { IM,  new Country("Isle of Man", "IM", "IMN", "833")},
            { IL,  new Country("Israel", "IL", "ISR", "376")},
            { IT,  new Country("Italy", "IT", "ITA", "380")},
            { JM,  new Country("Jamaica", "JM", "JAM", "388")},
            { JP,  new Country("Japan", "JP", "JPN", "392")},
            { JE,  new Country("Jersey", "JE", "JEY", "832")},
            { JO,  new Country("Jordan", "JO", "JOR", "400")},
            { KZ,  new Country("Kazakhstan", "KZ", "KAZ", "398")},
            { KE,  new Country("Kenya", "KE", "KEN", "404")},
            { KI,  new Country("Kiribati", "KI", "KIR", "296")},
            { KP,  new Country("Korea (Democratic People's Republic of)", "KP", "PRK", "408")},
            { KR,  new Country("Korea, Republic of", "KR", "KOR", "410")},
            { KW,  new Country("Kuwait", "KW", "KWT", "414")},
            { KG,  new Country("Kyrgyzstan", "KG", "KGZ", "417")},
            { LA,  new Country("Lao People's Democratic Republic", "LA", "LAO", "418")},
            { LV,  new Country("Latvia", "LV", "LVA", "428")},
            { LB,  new Country("Lebanon", "LB", "LBN", "422")},
            { LS,  new Country("Lesotho", "LS", "LSO", "426")},
            { LR,  new Country("Liberia", "LR", "LBR", "430")},
            { LY,  new Country("Libya", "LY", "LBY", "434")},
            { LI,  new Country("Liechtenstein", "LI", "LIE", "438")},
            { LT,  new Country("Lithuania", "LT", "LTU", "440")},
            { LU,  new Country("Luxembourg", "LU", "LUX", "442")},
            { MO,  new Country("Macao", "MO", "MAC", "446")},
            { MG,  new Country("Madagascar", "MG", "MDG", "450")},
            { MW,  new Country("Malawi", "MW", "MWI", "454")},
            { MY,  new Country("Malaysia", "MY", "MYS", "458")},
            { MV,  new Country("Maldives", "MV", "MDV", "462")},
            { ML,  new Country("Mali", "ML", "MLI", "466")},
            { MT,  new Country("Malta", "MT", "MLT", "470")},
            { MH,  new Country("Marshall Islands", "MH", "MHL", "584")},
            { MQ,  new Country("Martinique", "MQ", "MTQ", "474")},
            { MR,  new Country("Mauritania", "MR", "MRT", "478")},
            { MU,  new Country("Mauritius", "MU", "MUS", "480")},
            { YT,  new Country("Mayotte", "YT", "MYT", "175")},
            { MX,  new Country("Mexico", "MX", "MEX", "484")},
            { FM,  new Country("Micronesia (Federated States of)", "FM", "FSM", "583")},
            { MD,  new Country("Moldova, Republic of", "MD", "MDA", "498")},
            { MC,  new Country("Monaco", "MC", "MCO", "492")},
            { MN,  new Country("Mongolia", "MN", "MNG", "496")},
            { ME,  new Country("Montenegro", "ME", "MNE", "499")},
            { MS,  new Country("Montserrat", "MS", "MSR", "500")},
            { MA,  new Country("Morocco", "MA", "MAR", "504")},
            { MZ,  new Country("Mozambique", "MZ", "MOZ", "508")},
            { MM,  new Country("Myanmar", "MM", "MMR", "104")},
            { NA,  new Country("Namibia", "NA", "NAM", "516")},
            { NR,  new Country("Nauru", "NR", "NRU", "520")},
            { NP,  new Country("Nepal", "NP", "NPL", "524")},
            { NL,  new Country("Netherlands", "NL", "NLD", "528")},
            { NC,  new Country("New Caledonia", "NC", "NCL", "540")},
            { NZ,  new Country("New Zealand", "NZ", "NZL", "554")},
            { NI,  new Country("Nicaragua", "NI", "NIC", "558")},
            { NE,  new Country("Niger", "NE", "NER", "562")},
            { NG,  new Country("Nigeria", "NG", "NGA", "566")},
            { NU,  new Country("Niue", "NU", "NIU", "570")},
            { NF,  new Country("Norfolk Island", "NF", "NFK", "574")},
            { MK,  new Country("North Macedonia", "MK", "MKD", "807")},
            { MP,  new Country("Northern Mariana Islands", "MP", "MNP", "580")},
            { NO,  new Country("Norway", "NO", "NOR", "578")},
            { OM,  new Country("Oman", "OM", "OMN", "512")},
            { PK,  new Country("Pakistan", "PK", "PAK", "586")},
            { PW,  new Country("Palau", "PW", "PLW", "585")},
            { PS,  new Country("Palestine, State of", "PS", "PSE", "275")},
            { PA,  new Country("Panama", "PA", "PAN", "591")},
            { PG,  new Country("Papua New Guinea", "PG", "PNG", "598")},
            { PY,  new Country("Paraguay", "PY", "PRY", "600")},
            { PE,  new Country("Peru", "PE", "PER", "604")},
            { PH,  new Country("Philippines", "PH", "PHL", "608")},
            { PN,  new Country("Pitcairn", "PN", "PCN", "612")},
            { PL,  new Country("Poland", "PL", "POL", "616")},
            { PT,  new Country("Portugal", "PT", "PRT", "620")},
            { PR,  new Country("Puerto Rico", "PR", "PRI", "630")},
            { QA,  new Country("Qatar", "QA", "QAT", "634")},
            { RE,  new Country("RÃ©union", "RE", "REU", "638")},
            { RO,  new Country("Romania", "RO", "ROU", "642")},
            { RU,  new Country("Russian Federation", "RU", "RUS", "643")},
            { RW,  new Country("Rwanda", "RW", "RWA", "646")},
            { BL,  new Country("Saint BarthÃ©lemy", "BL", "BLM", "652")},
            { SH,  new Country("Saint Helena, Ascension and Tristan da Cunha", "SH", "SHN", "654")},
            { KN,  new Country("Saint Kitts and Nevis", "KN", "KNA", "659")},
            { LC,  new Country("Saint Lucia", "LC", "LCA", "662")},
            { MF,  new Country("Saint Martin (French part)", "MF", "MAF", "663")},
            { PM,  new Country("Saint Pierre and Miquelon", "PM", "SPM", "666")},
            { VC,  new Country("Saint Vincent and the Grenadines", "VC", "VCT", "670")},
            { WS,  new Country("Samoa", "WS", "WSM", "882")},
            { SM,  new Country("San Marino", "SM", "SMR", "674")},
            { ST,  new Country("Sao Tome and Principe", "ST", "STP", "678")},
            { SA,  new Country("Saudi Arabia", "SA", "SAU", "682")},
            { SN,  new Country("Senegal", "SN", "SEN", "686")},
            { RS,  new Country("Serbia", "RS", "SRB", "688")},
            { SC,  new Country("Seychelles", "SC", "SYC", "690")},
            { SL,  new Country("Sierra Leone", "SL", "SLE", "694")},
            { SG,  new Country("Singapore", "SG", "SGP", "702")},
            { SX,  new Country("Sint Maarten (Dutch part)", "SX", "SXM", "534")},
            { SK,  new Country("Slovakia", "SK", "SVK", "703")},
            { SI,  new Country("Slovenia", "SI", "SVN", "705")},
            { SB,  new Country("Solomon Islands", "SB", "SLB", "90")},
            { SO,  new Country("Somalia", "SO", "SOM", "706")},
            { ZA,  new Country("South Africa", "ZA", "ZAF", "710")},
            { GS,  new Country("South Georgia and the South Sandwich Islands", "GS", "SGS", "239")},
            { SS,  new Country("South Sudan", "SS", "SSD", "728")},
            { ES,  new Country("Spain", "ES", "ESP", "724")},
            { LK,  new Country("Sri Lanka", "LK", "LKA", "144")},
            { SD,  new Country("Sudan", "SD", "SDN", "729")},
            { SR,  new Country("Suriname", "SR", "SUR", "740")},
            { SJ,  new Country("Svalbard and Jan Mayen", "SJ", "SJM", "744")},
            { SE,  new Country("Sweden", "SE", "SWE", "752")},
            { CH,  new Country("Switzerland", "CH", "CHE", "756")},
            { SY,  new Country("Syrian Arab Republic", "SY", "SYR", "760")},
            { TW,  new Country("Taiwan, Province of China", "TW", "TWN", "158")},
            { TJ,  new Country("Tajikistan", "TJ", "TJK", "762")},
            { TZ,  new Country("Tanzania, United Republic of", "TZ", "TZA", "834")},
            { TH,  new Country("Thailand", "TH", "THA", "764")},
            { TL,  new Country("Timor-Leste", "TL", "TLS", "626")},
            { TG,  new Country("Togo", "TG", "TGO", "768")},
            { TK,  new Country("Tokelau", "TK", "TKL", "772")},
            { TO,  new Country("Tonga", "TO", "TON", "776")},
            { TT,  new Country("Trinidad and Tobago", "TT", "TTO", "780")},
            { TN,  new Country("Tunisia", "TN", "TUN", "788")},
            { TR,  new Country("Turkey", "TR", "TUR", "792")},
            { TM,  new Country("Turkmenistan", "TM", "TKM", "795")},
            { TC,  new Country("Turks and Caicos Islands", "TC", "TCA", "796")},
            { TV,  new Country("Tuvalu", "TV", "TUV", "798")},
            { UG,  new Country("Uganda", "UG", "UGA", "800")},
            { UA,  new Country("Ukraine", "UA", "UKR", "804")},
            { AE,  new Country("United Arab Emirates", "AE", "ARE", "784")},
            { GB,  new Country("United Kingdom of Great Britain and Northern Ireland", "GB", "GBR", "826")},
            { US,  new Country("United States of America", "US", "USA", "840")},
            { UM,  new Country("United States Minor Outlying Islands", "UM", "UMI", "581")},
            { UY,  new Country("Uruguay", "UY", "URY", "858")},
            { UZ,  new Country("Uzbekistan", "UZ", "UZB", "860")},
            { VU,  new Country("Vanuatu", "VU", "VUT", "548")},
            { VE,  new Country("Venezuela (Bolivarian Republic of)", "VE", "VEN", "862")},
            { VN,  new Country("Viet Nam", "VN", "VNM", "704")},
            { VG,  new Country("Virgin Islands (British)", "VG", "VGB", "92")},
            { VI,  new Country("Virgin Islands (U.S.)", "VI", "VIR", "850")},
            { WF,  new Country("Wallis and Futuna", "WF", "WLF", "876")},
            { EH,  new Country("Western Sahara", "EH", "ESH", "732")},
            { YE,  new Country("Yemen", "YE", "YEM", "887")},
            { ZM,  new Country("Zambia", "ZM", "ZMB", "894")},
            { ZW,  new Country("Zimbabwe", "ZW", "ZWE", "716")},
        };


        /// <summary>
        /// Afghanistan
        /// </summary>
        public const string AF = "AF";
        /// <summary>
        /// Åland Islands
        /// </summary>
        public const string AX = "AX";
        /// <summary>
        /// Albania
        /// </summary>
        public const string AL = "AL";
        /// <summary>
        /// Algeria
        /// </summary>
        public const string DZ = "DZ";
        /// <summary>
        /// American Samoa
        /// </summary>
        public const string AS = "AS";
        /// <summary>
        /// Andorra
        /// </summary>
        public const string AD = "AD";
        /// <summary>
        /// Angola
        /// </summary>
        public const string AO = "AO";
        /// <summary>
        /// Anguilla
        /// </summary>
        public const string AI = "AI";
        /// <summary>
        /// Antarctica
        /// </summary>
        public const string AQ = "AQ";
        /// <summary>
        /// Antigua and Barbuda
        /// </summary>
        public const string AG = "AG";
        /// <summary>
        /// Argentina
        /// </summary>
        public const string AR = "AR";
        /// <summary>
        /// Armenia
        /// </summary>
        public const string AM = "AM";
        /// <summary>
        /// Aruba
        /// </summary>
        public const string AW = "AW";
        /// <summary>
        /// Australia
        /// </summary>
        public const string AU = "AU";
        /// <summary>
        /// Austria
        /// </summary>
        public const string AT = "AT";
        /// <summary>
        /// Azerbaijan
        /// </summary>
        public const string AZ = "AZ";
        /// <summary>
        /// Bahamas
        /// </summary>
        public const string BS = "BS";
        /// <summary>
        /// Bahrain
        /// </summary>
        public const string BH = "BH";
        /// <summary>
        /// Bangladesh
        /// </summary>
        public const string BD = "BD";
        /// <summary>
        /// Barbados
        /// </summary>
        public const string BB = "BB";
        /// <summary>
        /// Belarus
        /// </summary>
        public const string BY = "BY";
        /// <summary>
        /// Belgium
        /// </summary>
        public const string BE = "BE";
        /// <summary>
        /// Belize
        /// </summary>
        public const string BZ = "BZ";
        /// <summary>
        /// Benin
        /// </summary>
        public const string BJ = "BJ";
        /// <summary>
        /// Bermuda
        /// </summary>
        public const string BM = "BM";
        /// <summary>
        /// Bhutan
        /// </summary>
        public const string BT = "BT";
        /// <summary>
        /// Bolivia (Plurinational State of)
        /// </summary>
        public const string BO = "BO";
        /// <summary>
        /// Bonaire, Sint Eustatius and Saba
        /// </summary>
        public const string BQ = "BQ";
        /// <summary>
        /// Bosnia and Herzegovina
        /// </summary>
        public const string BA = "BA";
        /// <summary>
        /// Botswana
        /// </summary>
        public const string BW = "BW";
        /// <summary>
        /// Bouvet Island
        /// </summary>
        public const string BV = "BV";
        /// <summary>
        /// Brazil
        /// </summary>
        public const string BR = "BR";
        /// <summary>
        /// British Indian Ocean Territory
        /// </summary>
        public const string IO = "IO";
        /// <summary>
        /// Brunei Darussalam
        /// </summary>
        public const string BN = "BN";
        /// <summary>
        /// Bulgaria
        /// </summary>
        public const string BG = "BG";
        /// <summary>
        /// Burkina Faso
        /// </summary>
        public const string BF = "BF";
        /// <summary>
        /// Burundi
        /// </summary>
        public const string BI = "BI";
        /// <summary>
        /// Cabo Verde
        /// </summary>
        public const string CV = "CV";
        /// <summary>
        /// Cambodia
        /// </summary>
        public const string KH = "KH";
        /// <summary>
        /// Cameroon
        /// </summary>
        public const string CM = "CM";
        /// <summary>
        /// Canada
        /// </summary>
        public const string CA = "CA";
        /// <summary>
        /// Cayman Islands
        /// </summary>
        public const string KY = "KY";
        /// <summary>
        /// Central African Republic
        /// </summary>
        public const string CF = "CF";
        /// <summary>
        /// Chad
        /// </summary>
        public const string TD = "TD";
        /// <summary>
        /// Chile
        /// </summary>
        public const string CL = "CL";
        /// <summary>
        /// China
        /// </summary>
        public const string CN = "CN";
        /// <summary>
        /// Christmas Island
        /// </summary>
        public const string CX = "CX";
        /// <summary>
        /// Cocos (Keeling) Islands
        /// </summary>
        public const string CC = "CC";
        /// <summary>
        /// Colombia
        /// </summary>
        public const string CO = "CO";
        /// <summary>
        /// Comoros
        /// </summary>
        public const string KM = "KM";
        /// <summary>
        /// Congo
        /// </summary>
        public const string CG = "CG";
        /// <summary>
        /// Congo, Democratic Republic of the
        /// </summary>
        public const string CD = "CD";
        /// <summary>
        /// Cook Islands
        /// </summary>
        public const string CK = "CK";
        /// <summary>
        /// Costa Rica
        /// </summary>
        public const string CR = "CR";
        /// <summary>
        /// CÃ´te d'Ivoire
        /// </summary>
        public const string CI = "CI";
        /// <summary>
        /// Croatia
        /// </summary>
        public const string HR = "HR";
        /// <summary>
        /// Cuba
        /// </summary>
        public const string CU = "CU";
        /// <summary>
        /// CuraÃ§ao
        /// </summary>
        public const string CW = "CW";
        /// <summary>
        /// Cyprus
        /// </summary>
        public const string CY = "CY";
        /// <summary>
        /// Czechia
        /// </summary>
        public const string CZ = "CZ";
        /// <summary>
        /// Denmark
        /// </summary>
        public const string DK = "DK";
        /// <summary>
        /// Djibouti
        /// </summary>
        public const string DJ = "DJ";
        /// <summary>
        /// Dominica
        /// </summary>
        public const string DM = "DM";
        /// <summary>
        /// Dominican Republic
        /// </summary>
        public const string DO = "DO";
        /// <summary>
        /// Ecuador
        /// </summary>
        public const string EC = "EC";
        /// <summary>
        /// Egypt
        /// </summary>
        public const string EG = "EG";
        /// <summary>
        /// El Salvador
        /// </summary>
        public const string SV = "SV";
        /// <summary>
        /// Equatorial Guinea
        /// </summary>
        public const string GQ = "GQ";
        /// <summary>
        /// Eritrea
        /// </summary>
        public const string ER = "ER";
        /// <summary>
        /// Estonia
        /// </summary>
        public const string EE = "EE";
        /// <summary>
        /// Eswatini
        /// </summary>
        public const string SZ = "SZ";
        /// <summary>
        /// Ethiopia
        /// </summary>
        public const string ET = "ET";
        /// <summary>
        /// Falkland Islands (Malvinas)
        /// </summary>
        public const string FK = "FK";
        /// <summary>
        /// Faroe Islands
        /// </summary>
        public const string FO = "FO";
        /// <summary>
        /// Fiji
        /// </summary>
        public const string FJ = "FJ";
        /// <summary>
        /// Finland
        /// </summary>
        public const string FI = "FI";
        /// <summary>
        /// France
        /// </summary>
        public const string FR = "FR";
        /// <summary>
        /// French Guiana
        /// </summary>
        public const string GF = "GF";
        /// <summary>
        /// French Polynesia
        /// </summary>
        public const string PF = "PF";
        /// <summary>
        /// French Southern Territories
        /// </summary>
        public const string TF = "TF";
        /// <summary>
        /// Gabon
        /// </summary>
        public const string GA = "GA";
        /// <summary>
        /// Gambia
        /// </summary>
        public const string GM = "GM";
        /// <summary>
        /// Georgia
        /// </summary>
        public const string GE = "GE";
        /// <summary>
        /// Germany
        /// </summary>
        public const string DE = "DE";
        /// <summary>
        /// Ghana
        /// </summary>
        public const string GH = "GH";
        /// <summary>
        /// Gibraltar
        /// </summary>
        public const string GI = "GI";
        /// <summary>
        /// Greece
        /// </summary>
        public const string GR = "GR";
        /// <summary>
        /// Greenland
        /// </summary>
        public const string GL = "GL";
        /// <summary>
        /// Grenada
        /// </summary>
        public const string GD = "GD";
        /// <summary>
        /// Guadeloupe
        /// </summary>
        public const string GP = "GP";
        /// <summary>
        /// Guam
        /// </summary>
        public const string GU = "GU";
        /// <summary>
        /// Guatemala
        /// </summary>
        public const string GT = "GT";
        /// <summary>
        /// Guernsey
        /// </summary>
        public const string GG = "GG";
        /// <summary>
        /// Guinea
        /// </summary>
        public const string GN = "GN";
        /// <summary>
        /// Guinea-Bissau
        /// </summary>
        public const string GW = "GW";
        /// <summary>
        /// Guyana
        /// </summary>
        public const string GY = "GY";
        /// <summary>
        /// Haiti
        /// </summary>
        public const string HT = "HT";
        /// <summary>
        /// Heard Island and McDonald Islands
        /// </summary>
        public const string HM = "HM";
        /// <summary>
        /// Holy See
        /// </summary>
        public const string VA = "VA";
        /// <summary>
        /// Honduras
        /// </summary>
        public const string HN = "HN";
        /// <summary>
        /// Hong Kong
        /// </summary>
        public const string HK = "HK";
        /// <summary>
        /// Hungary
        /// </summary>
        public const string HU = "HU";
        /// <summary>
        /// Iceland
        /// </summary>
        public const string IS = "IS";
        /// <summary>
        /// India
        /// </summary>
        public const string IN = "IN";
        /// <summary>
        /// Indonesia
        /// </summary>
        public const string ID = "ID";
        /// <summary>
        /// Iran (Islamic Republic of)
        /// </summary>
        public const string IR = "IR";
        /// <summary>
        /// Iraq
        /// </summary>
        public const string IQ = "IQ";
        /// <summary>
        /// Ireland
        /// </summary>
        public const string IE = "IE";
        /// <summary>
        /// Isle of Man
        /// </summary>
        public const string IM = "IM";
        /// <summary>
        /// Israel
        /// </summary>
        public const string IL = "IL";
        /// <summary>
        /// Italy
        /// </summary>
        public const string IT = "IT";
        /// <summary>
        /// Jamaica
        /// </summary>
        public const string JM = "JM";
        /// <summary>
        /// Japan
        /// </summary>
        public const string JP = "JP";
        /// <summary>
        /// Jersey
        /// </summary>
        public const string JE = "JE";
        /// <summary>
        /// Jordan
        /// </summary>
        public const string JO = "JO";
        /// <summary>
        /// Kazakhstan
        /// </summary>
        public const string KZ = "KZ";
        /// <summary>
        /// Kenya
        /// </summary>
        public const string KE = "KE";
        /// <summary>
        /// Kiribati
        /// </summary>
        public const string KI = "KI";
        /// <summary>
        /// Korea (Democratic People's Republic of)
        /// </summary>
        public const string KP = "KP";
        /// <summary>
        /// Korea, Republic of
        /// </summary>
        public const string KR = "KR";
        /// <summary>
        /// Kuwait
        /// </summary>
        public const string KW = "KW";
        /// <summary>
        /// Kyrgyzstan
        /// </summary>
        public const string KG = "KG";
        /// <summary>
        /// Lao People's Democratic Republic
        /// </summary>
        public const string LA = "LA";
        /// <summary>
        /// Latvia
        /// </summary>
        public const string LV = "LV";
        /// <summary>
        /// Lebanon
        /// </summary>
        public const string LB = "LB";
        /// <summary>
        /// Lesotho
        /// </summary>
        public const string LS = "LS";
        /// <summary>
        /// Liberia
        /// </summary>
        public const string LR = "LR";
        /// <summary>
        /// Libya
        /// </summary>
        public const string LY = "LY";
        /// <summary>
        /// Liechtenstein
        /// </summary>
        public const string LI = "LI";
        /// <summary>
        /// Lithuania
        /// </summary>
        public const string LT = "LT";
        /// <summary>
        /// Luxembourg
        /// </summary>
        public const string LU = "LU";
        /// <summary>
        /// Macao
        /// </summary>
        public const string MO = "MO";
        /// <summary>
        /// Madagascar
        /// </summary>
        public const string MG = "MG";
        /// <summary>
        /// Malawi
        /// </summary>
        public const string MW = "MW";
        /// <summary>
        /// Malaysia
        /// </summary>
        public const string MY = "MY";
        /// <summary>
        /// Maldives
        /// </summary>
        public const string MV = "MV";
        /// <summary>
        /// Mali
        /// </summary>
        public const string ML = "ML";
        /// <summary>
        /// Malta
        /// </summary>
        public const string MT = "MT";
        /// <summary>
        /// Marshall Islands
        /// </summary>
        public const string MH = "MH";
        /// <summary>
        /// Martinique
        /// </summary>
        public const string MQ = "MQ";
        /// <summary>
        /// Mauritania
        /// </summary>
        public const string MR = "MR";
        /// <summary>
        /// Mauritius
        /// </summary>
        public const string MU = "MU";
        /// <summary>
        /// Mayotte
        /// </summary>
        public const string YT = "YT";
        /// <summary>
        /// Mexico
        /// </summary>
        public const string MX = "MX";
        /// <summary>
        /// Micronesia (Federated States of)
        /// </summary>
        public const string FM = "FM";
        /// <summary>
        /// Moldova, Republic of
        /// </summary>
        public const string MD = "MD";
        /// <summary>
        /// Monaco
        /// </summary>
        public const string MC = "MC";
        /// <summary>
        /// Mongolia
        /// </summary>
        public const string MN = "MN";
        /// <summary>
        /// Montenegro
        /// </summary>
        public const string ME = "ME";
        /// <summary>
        /// Montserrat
        /// </summary>
        public const string MS = "MS";
        /// <summary>
        /// Morocco
        /// </summary>
        public const string MA = "MA";
        /// <summary>
        /// Mozambique
        /// </summary>
        public const string MZ = "MZ";
        /// <summary>
        /// Myanmar
        /// </summary>
        public const string MM = "MM";
        /// <summary>
        /// Namibia
        /// </summary>
        public const string NA = "NA";
        /// <summary>
        /// Nauru
        /// </summary>
        public const string NR = "NR";
        /// <summary>
        /// Nepal
        /// </summary>
        public const string NP = "NP";
        /// <summary>
        /// Netherlands
        /// </summary>
        public const string NL = "NL";
        /// <summary>
        /// New Caledonia
        /// </summary>
        public const string NC = "NC";
        /// <summary>
        /// New Zealand
        /// </summary>
        public const string NZ = "NZ";
        /// <summary>
        /// Nicaragua
        /// </summary>
        public const string NI = "NI";
        /// <summary>
        /// Niger
        /// </summary>
        public const string NE = "NE";
        /// <summary>
        /// Nigeria
        /// </summary>
        public const string NG = "NG";
        /// <summary>
        /// Niue
        /// </summary>
        public const string NU = "NU";
        /// <summary>
        /// Norfolk Island
        /// </summary>
        public const string NF = "NF";
        /// <summary>
        /// North Macedonia
        /// </summary>
        public const string MK = "MK";
        /// <summary>
        /// Northern Mariana Islands
        /// </summary>
        public const string MP = "MP";
        /// <summary>
        /// Norway
        /// </summary>
        public const string NO = "NO";
        /// <summary>
        /// Oman
        /// </summary>
        public const string OM = "OM";
        /// <summary>
        /// Pakistan
        /// </summary>
        public const string PK = "PK";
        /// <summary>
        /// Palau
        /// </summary>
        public const string PW = "PW";
        /// <summary>
        /// Palestine, State of
        /// </summary>
        public const string PS = "PS";
        /// <summary>
        /// Panama
        /// </summary>
        public const string PA = "PA";
        /// <summary>
        /// Papua New Guinea
        /// </summary>
        public const string PG = "PG";
        /// <summary>
        /// Paraguay
        /// </summary>
        public const string PY = "PY";
        /// <summary>
        /// Peru
        /// </summary>
        public const string PE = "PE";
        /// <summary>
        /// Philippines
        /// </summary>
        public const string PH = "PH";
        /// <summary>
        /// Pitcairn
        /// </summary>
        public const string PN = "PN";
        /// <summary>
        /// Poland
        /// </summary>
        public const string PL = "PL";
        /// <summary>
        /// Portugal
        /// </summary>
        public const string PT = "PT";
        /// <summary>
        /// Puerto Rico
        /// </summary>
        public const string PR = "PR";
        /// <summary>
        /// Qatar
        /// </summary>
        public const string QA = "QA";
        /// <summary>
        /// RÃ©union
        /// </summary>
        public const string RE = "RE";
        /// <summary>
        /// Romania
        /// </summary>
        public const string RO = "RO";
        /// <summary>
        /// Russian Federation
        /// </summary>
        public const string RU = "RU";
        /// <summary>
        /// Rwanda
        /// </summary>
        public const string RW = "RW";
        /// <summary>
        /// Saint BarthÃ©lemy
        /// </summary>
        public const string BL = "BL";
        /// <summary>
        /// Saint Helena, Ascension and Tristan da Cunha
        /// </summary>
        public const string SH = "SH";
        /// <summary>
        /// Saint Kitts and Nevis
        /// </summary>
        public const string KN = "KN";
        /// <summary>
        /// Saint Lucia
        /// </summary>
        public const string LC = "LC";
        /// <summary>
        /// Saint Martin (French part)
        /// </summary>
        public const string MF = "MF";
        /// <summary>
        /// Saint Pierre and Miquelon
        /// </summary>
        public const string PM = "PM";
        /// <summary>
        /// Saint Vincent and the Grenadines
        /// </summary>
        public const string VC = "VC";
        /// <summary>
        /// Samoa
        /// </summary>
        public const string WS = "WS";
        /// <summary>
        /// San Marino
        /// </summary>
        public const string SM = "SM";
        /// <summary>
        /// Sao Tome and Principe
        /// </summary>
        public const string ST = "ST";
        /// <summary>
        /// Saudi Arabia
        /// </summary>
        public const string SA = "SA";
        /// <summary>
        /// Senegal
        /// </summary>
        public const string SN = "SN";
        /// <summary>
        /// Serbia
        /// </summary>
        public const string RS = "RS";
        /// <summary>
        /// Seychelles
        /// </summary>
        public const string SC = "SC";
        /// <summary>
        /// Sierra Leone
        /// </summary>
        public const string SL = "SL";
        /// <summary>
        /// Singapore
        /// </summary>
        public const string SG = "SG";
        /// <summary>
        /// Sint Maarten (Dutch part)
        /// </summary>
        public const string SX = "SX";
        /// <summary>
        /// Slovakia
        /// </summary>
        public const string SK = "SK";
        /// <summary>
        /// Slovenia
        /// </summary>
        public const string SI = "SI";
        /// <summary>
        /// Solomon Islands
        /// </summary>
        public const string SB = "SB";
        /// <summary>
        /// Somalia
        /// </summary>
        public const string SO = "SO";
        /// <summary>
        /// South Africa
        /// </summary>
        public const string ZA = "ZA";
        /// <summary>
        /// South Georgia and the South Sandwich Islands
        /// </summary>
        public const string GS = "GS";
        /// <summary>
        /// South Sudan
        /// </summary>
        public const string SS = "SS";
        /// <summary>
        /// Spain
        /// </summary>
        public const string ES = "ES";
        /// <summary>
        /// Sri Lanka
        /// </summary>
        public const string LK = "LK";
        /// <summary>
        /// Sudan
        /// </summary>
        public const string SD = "SD";
        /// <summary>
        /// Suriname
        /// </summary>
        public const string SR = "SR";
        /// <summary>
        /// Svalbard and Jan Mayen
        /// </summary>
        public const string SJ = "SJ";
        /// <summary>
        /// Sweden
        /// </summary>
        public const string SE = "SE";
        /// <summary>
        /// Switzerland
        /// </summary>
        public const string CH = "CH";
        /// <summary>
        /// Syrian Arab Republic
        /// </summary>
        public const string SY = "SY";
        /// <summary>
        /// Taiwan, Province of China
        /// </summary>
        public const string TW = "TW";
        /// <summary>
        /// Tajikistan
        /// </summary>
        public const string TJ = "TJ";
        /// <summary>
        /// Tanzania, United Republic of
        /// </summary>
        public const string TZ = "TZ";
        /// <summary>
        /// Thailand
        /// </summary>
        public const string TH = "TH";
        /// <summary>
        /// Timor-Leste
        /// </summary>
        public const string TL = "TL";
        /// <summary>
        /// Togo
        /// </summary>
        public const string TG = "TG";
        /// <summary>
        /// Tokelau
        /// </summary>
        public const string TK = "TK";
        /// <summary>
        /// Tonga
        /// </summary>
        public const string TO = "TO";
        /// <summary>
        /// Trinidad and Tobago
        /// </summary>
        public const string TT = "TT";
        /// <summary>
        /// Tunisia
        /// </summary>
        public const string TN = "TN";
        /// <summary>
        /// Turkey
        /// </summary>
        public const string TR = "TR";
        /// <summary>
        /// Turkmenistan
        /// </summary>
        public const string TM = "TM";
        /// <summary>
        /// Turks and Caicos Islands
        /// </summary>
        public const string TC = "TC";
        /// <summary>
        /// Tuvalu
        /// </summary>
        public const string TV = "TV";
        /// <summary>
        /// Uganda
        /// </summary>
        public const string UG = "UG";
        /// <summary>
        /// Ukraine
        /// </summary>
        public const string UA = "UA";
        /// <summary>
        /// United Arab Emirates
        /// </summary>
        public const string AE = "AE";
        /// <summary>
        /// United Kingdom of Great Britain and Northern Ireland
        /// </summary>
        public const string GB = "GB";
        /// <summary>
        /// United States of America
        /// </summary>
        public const string US = "US";
        /// <summary>
        /// United States Minor Outlying Islands
        /// </summary>
        public const string UM = "UM";
        /// <summary>
        /// Uruguay
        /// </summary>
        public const string UY = "UY";
        /// <summary>
        /// Uzbekistan
        /// </summary>
        public const string UZ = "UZ";
        /// <summary>
        /// Vanuatu
        /// </summary>
        public const string VU = "VU";
        /// <summary>
        /// Venezuela (Bolivarian Republic of)
        /// </summary>
        public const string VE = "VE";
        /// <summary>
        /// Viet Nam
        /// </summary>
        public const string VN = "VN";
        /// <summary>
        /// Virgin Islands (British)
        /// </summary>
        public const string VG = "VG";
        /// <summary>
        /// Virgin Islands (U.S.)
        /// </summary>
        public const string VI = "VI";
        /// <summary>
        /// Wallis and Futuna
        /// </summary>
        public const string WF = "WF";
        /// <summary>
        /// Western Sahara
        /// </summary>
        public const string EH = "EH";
        /// <summary>
        /// Yemen
        /// </summary>
        public const string YE = "YE";
        /// <summary>
        /// Zambia
        /// </summary>
        public const string ZM = "ZM";
        /// <summary>
        /// Zimbabwe
        /// </summary>
        public const string ZW = "ZW";

    }
}

