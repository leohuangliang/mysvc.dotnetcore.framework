using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MySvc.Framework.Domain.Core.Models
{
    /// <summary>
    /// 货币 ISO 4217
    /// </summary>
    public enum Currency
    {
        [Description("阿联酋迪尔汗")]
        AED = 784,

        [Description("阿富汗尼")]
        AFN = 971,

        [Description("阿尔巴尼亚列克")]
        ALL = 008,

        [Description("亚美尼亚德拉姆")]
        AMD = 051,

        [Description("荷属安的列斯盾")]
        ANG = 532,

        [Description("宽扎")]
        AOA = 973,

        [Description("阿根廷比索")]
        ARS = 032,

        [Description("澳大利亚元")]
        AUD = 036,

        [Description("阿鲁巴弗洛林")]
        AWG = 533,

        [Description("阿塞拜疆马纳特")]
        AZN = 944,

        [Description("波斯尼亚和黑塞哥维那可兑换马克")]
        BAM = 977,

        [Description("巴巴多斯元")]
        BBD = 052,

        [Description("孟加拉塔卡")]
        BDT = 050,

        [Description("保加利亚列弗")]
        BGN = 975,

        [Description("巴林第纳尔")]
        BHD = 048,

        [Description("布隆迪法郎")]
        BIF = 108,

        [Description("百慕大元")]
        BMD = 060,

        [Description("文莱元")]
        BND = 096,

        [Description("玻利维亚诺")]
        BOB = 068,

        [Description("玻利维亚资金")]
        BOV = 984,

        [Description("巴西雷亚尔")]
        BRL = 986,

        [Description("巴哈马元")]
        BSD = 044,

        [Description("不丹努扎姆")]
        BTN = 064,

        [Description("博茨瓦纳普拉")]
        BWP = 072,

        [Description("白俄罗斯卢布")]
        BYR = 974,

        [Description("伯利兹元")]
        BZD = 084,

        [Description("加拿大元")]
        CAD = 124,

        [Description("刚果法郎")]
        CDF = 976,

        [Description("WIR欧元")]
        CHE = 947,

        [Description("")]
        CHF = 756,

        [Description("WIR法郎")]
        CHW = 948,

        [Description("智利发展单位")]
        CLF = 990,

        [Description("智利比索")]
        CLP = 152,

        [Description("人民币")]
        CNY = 156,

        [Description("人民币（离岸）")]
        CNH =157,

        [Description("哥伦比亚比索")]
        COP = 170,

        [Description("货币英雄联盟(UVR)(基金代码)")]
        COU = 970,

        [Description("哥斯达黎加科朗")]
        CRC = 188,

        [Description("古巴可兑换比索")]
        CUC = 931,

        [Description("古巴比索")]
        CUP = 192,

        [Description("佛得角埃斯库多")]
        CVE = 132,

        [Description("捷克克朗")]
        CZK = 203,

        [Description("吉布提法郎")]
        DJF = 262,

        [Description("丹麦克朗")]
        DKK = 208,

        [Description("多米尼加比索")]
        DOP = 214,

        [Description("阿尔及利亚第纳尔")]
        DZD = 012,

        [Description("埃及磅")]
        EGP = 818,

        [Description("厄立特里亚纳克法")]
        ERN = 232,

        [Description("埃塞俄比亚比尔")]
        ETB = 230,

        [Description("欧元")]
        EUR = 978,

        [Description("斐济元")]
        FJD = 242,

        [Description("福克兰镑")]
        FKP = 238,

        [Description("英镑")]
        GBP = 826,

        [Description("格鲁吉亚拉里")]
        GEL = 981,

        [Description("加纳塞地")]
        GHS = 936,

        [Description("直布罗陀镑")]
        GIP = 292,

        [Description("冈比亚达拉西")]
        GMD = 270,

        [Description("几内亚法郎")]
        GNF = 324,

        [Description("危地马拉格查尔")]
        GTQ = 320,

        [Description("圭亚那元")]
        GYD = 328,

        [Description("港币")]
        HKD = 344,

        [Description("洪都拉斯伦皮拉")]
        HNL = 340,

        [Description("克罗地亚库纳")]
        HRK = 191,

        [Description("海地古德")]
        HTG = 332,

        [Description("匈牙利福林")]
        HUF = 348,

        [Description("印尼盾")]
        IDR = 360,

        [Description("以色列新谢克尔")]
        ILS = 376,

        [Description("印度卢比")]
        INR = 356,

        [Description("伊拉克第纳尔")]
        IQD = 368,

        [Description("伊朗里亚尔")]
        IRR = 364,

        [Description("冰岛克朗")]
        ISK = 352,

        [Description("牙买加元")]
        JMD = 388,

        [Description("约旦第纳尔")]
        JOD = 400,

        [Description("日元")]
        JPY = 392,

        [Description("肯尼亚先令")]
        KES = 404,

        [Description("吉尔吉斯斯坦索姆")]
        KGS = 417,

        [Description("柬埔寨瑞尔")]
        KHR = 116,

        [Description("科摩罗法郎")]
        KMF = 174,

        [Description("朝鲜元")]
        KPW = 408,

        [Description("韩元")]
        KRW = 410,

        [Description("科威特第纳尔")]
        KWD = 414,

        [Description("开曼群岛元")]
        KYD = 136,

        [Description("哈萨克斯坦坚戈")]
        KZT = 398,

        [Description("老挝基普")]
        LAK = 418,

        [Description("黎巴嫩镑")]
        LBP = 422,

        [Description("斯里兰卡卢比")]
        LKR = 144,

        [Description("利比里亚元")]
        LRD = 430,

        [Description("莱索托洛提")]
        LSL = 426,

        [Description("利比亚第纳尔")]
        LYD = 434,

        [Description("摩洛哥迪拉姆")]
        MAD = 504,

        [Description("摩尔多瓦列伊")]
        MDL = 498,

        [Description("马达加斯加阿里亚里")]
        MGA = 969,

        [Description("马其顿第纳尔")]
        MKD = 807,

        [Description("缅甸元")]
        MMK = 104,

        [Description("蒙古图格里克")]
        MNT = 496,

        [Description("澳门元")]
        MOP = 446,

        [Description("毛里塔尼亚乌吉亚")]
        MRO = 478,

        [Description("毛里求斯卢比")]
        MUR = 480,

        [Description("马尔代夫拉菲亚")]
        MVR = 462,

        [Description("马拉维克瓦查")]
        MWK = 454,

        [Description("墨西哥比索")]
        MXN = 484,

        [Description("墨西哥倒置基金(UDI)(基金代码)")]
        MXV = 979,

        [Description("马来西亚令吉")]
        MYR = 458,

        [Description("莫桑比克梅蒂卡尔")]
        MZN = 943,

        [Description("纳米比亚元")]
        NAD = 516,

        [Description("尼日利亚奈拉")]
        NGN = 566,

        [Description("科多巴")]
        NIO = 558,

        [Description("挪威克朗")]
        NOK = 578,

        [Description("尼泊尔卢比")]
        NPR = 524,

        [Description("新西兰元")]
        NZD = 554,

        [Description("阿曼里亚尔")]
        OMR = 512,

        [Description("巴拿马巴波亚")]
        PAB = 590,

        [Description("秘鲁新索尔")]
        PEN = 604,

        [Description("巴布亚新几内亚基那")]
        PGK = 598,

        [Description("菲律宾比索")]
        PHP = 608,

        [Description("巴基斯坦卢比")]
        PKR = 586,

        [Description("波兰兹罗提")]
        PLN = 985,

        [Description("巴拉圭瓜拉尼")]
        PYG = 600,

        [Description("卡塔尔里亚尔")]
        QAR = 634,

        [Description("罗马尼亚列伊")]
        RON = 946,

        [Description("塞尔维亚第纳尔")]
        RSD = 941,

        [Description("俄罗斯卢布")]
        RUB = 643,

        [Description("卢旺达法郎")]
        RWF = 646,

        [Description("沙特里亚尔")]
        SAR = 682,

        [Description("所罗门群岛元")]
        SBD = 090,

        [Description("塞舌尔卢比")]
        SCR = 690,

        [Description("苏丹镑")]
        SDG = 938,

        [Description("瑞典克朗")]
        SEK = 752,

        [Description("新加坡元")]
        SGD = 702,

        [Description("圣赫勒拿镑")]
        SHP = 654,

        [Description("塞拉利昂利昂")]
        SLL = 694,

        [Description("索马里先令")]
        SOS = 706,

        [Description("苏里南元")]
        SRD = 968,

        [Description("南苏丹镑")]
        SSP = 728,

        [Description("圣多美和普林西比多布拉")]
        STD = 678,

        [Description("萨尔瓦多科朗")]
        SVC = 222,

        [Description("叙利亚镑")]
        SYP = 760,

        [Description("斯威士兰里兰吉尼")]
        SZL = 748,

        [Description("泰铢")]
        THB = 764,

        [Description("塔吉克斯坦索莫尼")]
        TJS = 972,

        [Description("土库曼斯坦马纳特")]
        TMT = 934,

        [Description("突尼斯第纳尔")]
        TND = 788,

        [Description("汤加潘加")]
        TOP = 776,

        [Description("土耳其里拉")]
        TRY = 949,

        [Description("特立尼达多巴哥元")]
        TTD = 780,

        [Description("新台币")]
        TWD = 901,

        [Description("坦桑尼亚先令")]
        TZS = 834,

        [Description("乌克兰格里夫纳")]
        UAH = 980,

        [Description("乌干达先令")]
        UGX = 800,

        [Description("美元")]
        USD = 840,

        [Description("美元(次日)(资金代码)")]
        USN = 997,

        [Description("乌拉圭指数比索(URUIURUI)(基金代码)")]
        UYI = 940,

        [Description("乌拉圭比索")]
        UYU = 858,

        [Description("乌兹别克斯坦苏姆")]
        UZS = 860,

        [Description("委内瑞拉玻利瓦")]
        VEF = 937,

        [Description("越南盾")]
        VND = 704,

        [Description("瓦努阿图瓦图")]
        VUV = 548,

        [Description("萨摩亚塔拉")]
        WST = 882,

        [Description("中非金融合作法郎")]
        XAF = 950,

        [Description("银")]
        XAG = 961,

        [Description("金")]
        XAU = 959,

        [Description("欧洲复合单位")]
        XBA = 955,

        [Description("欧洲货币联盟")]
        XBB = 956,

        [Description("9号帐户的欧洲单位")]
        XBC = 957,

        [Description("17号帐户的欧洲单位")]
        XBD = 958,

        [Description("东加勒比元")]
        XCD = 951,

        [Description("国际货币基金组织的特别提款权")]
        XDR = 960,

        [Description("多哥非洲共同体法郎")]
        XOF = 952,

        [Description("钯(金衡制盎司)")]
        XPD = 964,

        [Description("太平洋法郎")]
        XPF = 953,

        [Description("铂(金衡制盎司)")]
        XPT = 962,

        [Description("区域补偿统一的系统")]
        XSU = 994,

        [Description("为测试保留的代码")]
        XTS = 963,

        [Description("亚行账户单位")]
        XUA = 965,

        [Description("非货币")]
        XXX = 999,

        [Description("也门里亚尔")]
        YER = 886,

        [Description("南非兰特")]
        ZAR = 710,

        [Description("赞比亚克瓦查")]
        ZMW = 967,

        [Description("津巴布韦元")]
        ZWL = 932

    }

    /// <summary>
    /// 
    /// </summary>
    public static class CurrencyHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currencyString"></param>
        /// <returns></returns>
        /// <exception cref="InvalidCastException"></exception>
        public static Currency GetCurrency(string currencyString)
        {


            bool ok = Enum.TryParse(currencyString, out Currency currency);
            if (ok) return currency;
            else
            {
                throw new InvalidCastException("Convert to Currency error");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currency"></param>
        /// <returns></returns>
        public static string GetCurrencyCode(this Currency currency)
        {
            string currencyCode = ((int)currency).ToString("000");
            return currencyCode;
        }
    }
}
