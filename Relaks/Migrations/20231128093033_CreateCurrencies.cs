﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Relaks.Migrations
{
    /// <inheritdoc />
    public partial class CreateCurrencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialCurrencies",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Symbol = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialCurrencies", x => x.Id);
                });

            migrationBuilder.Sql(CurrensiesSql);
        }

        private readonly string CurrensiesSql = @"insert into FinancialCurrencies (Id, Title, Symbol)
values  ('AED', 'Дирхам (дирхам ОАЭ)', '.د.إ • Dh'),
        ('AFN', 'Афгани', '؋ • Af'),
        ('ALL', 'Лек', 'L'),
        ('AMD', 'Драм (армянский драм)', '֏'),
        ('ANG', 'Гульден (нидерландский антильский гульден)', 'ƒ'),
        ('AOA', 'Кванза', 'Kz'),
        ('ARS', 'Песо (аргентинское песо)', '$'),
        ('AUD', 'Доллар (австралийский доллар)', '$'),
        ('AWG', 'Флорин (арубанский флорин)', 'ƒ'),
        ('AZN', 'Манат (азербайджанский манат)', '₼'),
        ('BAM', 'Марка (конвертируемая марка)', 'KM'),
        ('BBD', 'Доллар (барбадосский доллар)', '$'),
        ('BDT', 'Така', '৳'),
        ('BGN', 'Лев (болгарский лев)', 'лв'),
        ('BHD', 'Динар (бахрейнский динар)', '.د.ب • BD'),
        ('BIF', 'Франк (бурундийский франк)', '₣'),
        ('BMD', 'Доллар (бермудский доллар)', '$'),
        ('BND', 'Доллар (брунейский доллар)', '$'),
        ('BOB', 'Боливиано', '$'),
        ('BRL', 'Реал (бразильский реал)', '$'),
        ('BSD', 'Доллар (багамский доллар)', '$'),
        ('BTN', 'Нгултрум[13]', 'Nu'),
        ('BWP', 'Пула', 'P'),
        ('BYN', 'Рубль (белорусский рубль)', 'Br'),
        ('BZD', 'Доллар (белизский доллар)', '$'),
        ('CAD', 'Доллар (канадский доллар)', '$'),
        ('CDF', 'Франк (конголезский франк)', '₣'),
        ('CHF', 'Франк (швейцарский франк)', '₣'),
        ('CLP', 'Песо (чилийское песо)', '$'),
        ('CNY', 'Юань', '¥'),
        ('COP', 'Песо (колумбийское песо)', '$'),
        ('CRC', 'Колон (коста-риканский колон)', '₡'),
        ('CUP', 'Песо (кубинское песо)', '$'),
        ('CVE', 'Эскудо (эскудо Кабо-Верде)', '$'),
        ('CZK', 'Крона (чешская крона)', 'Kč'),
        ('DJF', 'Франк (франк Джибути)', '₣'),
        ('DKK', 'Крона (датская крона)', 'kr'),
        ('DOP', 'Песо (доминиканское песо)', '$'),
        ('DZD', 'Динар (алжирский динар)', '.د.ج • DA'),
        ('EGP', 'Фунт (египетский фунт)', '.ج.م • LE'),
        ('ERN', 'Накфа', 'Nfk'),
        ('ETB', 'Быр (эфиопский быр)', 'Br'),
        ('EUR', 'Евро', '€'),
        ('FJD', 'Доллар (доллар Фиджи)', '$'),
        ('FKP', 'Фунт (фунт Фолклендских островов)', '£'),
        ('GBP', 'Фунт (фунт стерлингов)', '£'),
        ('GEL', 'Лари', '₾'),
        ('GHS', 'Седи (ганский седи)', '₵'),
        ('GIP', 'Фунт (гибралтарский фунт)', '£'),
        ('GMD', 'Даласи', 'D'),
        ('GNF', 'Франк (гвинейский франк)', '₣'),
        ('GTQ', 'Кетсаль', 'Q'),
        ('GYD', 'Доллар (гайанский доллар)', '$'),
        ('HKD', 'Доллар (гонконгский доллар)', '$'),
        ('HNL', 'Лемпира', 'L'),
        ('HTG', 'Гурд[14]', 'G'),
        ('HUF', 'Форинт', 'Ft'),
        ('IDR', 'Рупия (индонезийская рупия)', 'Rp'),
        ('ILS', 'Шекель (новый израильский шекель)', '₪'),
        ('INR', 'Рупия (индийская рупия)', '₹'),
        ('IQD', 'Динар (иракский динар)', '.د.ع • ID'),
        ('IRR', 'Риал (иранский риал)', '﷼ • IR'),
        ('ISK', 'Крона (исландская крона)', 'kr'),
        ('JMD', 'Доллар (ямайский доллар)', '$'),
        ('JOD', 'Динар (иорданский динар)', '.د.إ • JD'),
        ('JPY', 'Иена', '¥'),
        ('KES', 'Шиллинг (кенийский шиллинг)', 'KSh'),
        ('KGS', 'Сом', 'с'),
        ('KHR', 'Риель', '៛'),
        ('KMF', 'Франк (франк Комор)', '₣'),
        ('KPW', 'Вона (северокорейская вона)', '원'),
        ('KRW', 'Вона (южнокорейская вона)', '₩'),
        ('KWD', 'Динар (кувейтский динар)', '.د.ك • KD'),
        ('KYD', 'Доллар (доллар Островов Кайман)', '$'),
        ('KZT', 'Тенге', '₸'),
        ('LAK', 'Кип', '₭'),
        ('LBP', 'Фунт (ливанский фунт)', '.ل.ل'),
        ('LKR', 'Рупия (шри-ланкийская рупия)', 'Re (мн. Rs)'),
        ('LRD', 'Доллар (либерийский доллар)', '$'),
        ('LSL', 'Лоти (мн. Малоти)', 'L'),
        ('LYD', 'Динар (ливийский динар)', '.د.ل • LD'),
        ('MAD', 'Дирхам (марокканский дирхам)', '.د.م • Dh'),
        ('MDL', 'Лей (молдавский лей)', 'L'),
        ('MGA', 'Ариари (малагасийский ариари)', 'Ar.'),
        ('MKD', 'Денар', 'ден.'),
        ('MMK', 'Кьят[24]', 'K'),
        ('MNT', 'Тугрик', '₮'),
        ('MOP', 'Патака', '$'),
        ('MRU', 'Угия', 'UM'),
        ('MUR', 'Рупия (маврикийская рупия)', 'Re (мн. Rs)'),
        ('MVR', 'Руфия', '.ރ • Rf'),
        ('MWK', 'Квача', 'K'),
        ('MXN', 'Песо (мексиканское песо)', '$'),
        ('MYR', 'Ринггит (малайзийский ринггит)', 'RM'),
        ('MZN', 'Метикал (мозамбикский метикал)', 'MT'),
        ('NAD', 'Доллар (доллар Намибии)', 'N$'),
        ('NGN', 'Найра', '₦'),
        ('NIO', 'Кордоба (золотая кордоба)', '$'),
        ('NOK', 'Крона (норвежская крона)', 'kr'),
        ('NPR', 'Рупия (непальская рупия)', 'Re (мн. Rs)'),
        ('NZD', 'Доллар (новозеландский доллар)', '$'),
        ('OMR', 'Риал (оманский риал)', '﷼ • RO'),
        ('PAB', 'Бальбоа[14]', 'B/.'),
        ('PEN', 'Соль (новый соль)', 'S/'),
        ('PGK', 'Кина', 'K'),
        ('PHP', 'Песо (филиппинское песо)', '₱'),
        ('PKR', 'Рупия (пакистанская рупия)', 'Re (мн. Rs)'),
        ('PLN', 'Злотый', 'zł'),
        ('PYG', 'Гуарани', '₲'),
        ('QAR', 'Риал (катарский риал)', '﷼ • QR'),
        ('RON', 'Лей (румынский лей)', 'L'),
        ('RSD', 'Динар (сербский динар)', 'din. • дин.'),
        ('RUB', 'Рубль (российский рубль)', '₽'),
        ('RWF', 'Франк (франк Руанды)', '₣'),
        ('SAR', 'Риял (саудовский риял)', '﷼ • SR'),
        ('SBD', 'Доллар (доллар Соломоновых Островов)', '$'),
        ('SCR', 'Рупия (сейшельская рупия)', 'Re (мн. Rs)'),
        ('SDG', 'Фунт (суданский фунт)', '£'),
        ('SEK', 'Крона (шведская крона)', 'kr'),
        ('SGD', 'Доллар (сингапурский доллар)', '$'),
        ('SHP', 'Фунт (фунт Святой Елены)', '£'),
        ('SLL', 'Леоне', 'Le'),
        ('SOS', 'Шиллинг (сомалийский шиллинг)', 'S'),
        ('SRD', 'Доллар (суринамский доллар)', '$'),
        ('SSP', 'Фунт (южносуданский фунт)', 'SSP'),
        ('STN', 'Добра', 'Db'),
        ('SVC', 'Колон (сальвадорский колон)', '₡'),
        ('SYP', 'Фунт (сирийский фунт)', '.ل.س • S£'),
        ('SZL', 'Лилангени (мн. Эмалангени)', 'L'),
        ('THB', 'Бат', '฿'),
        ('TJS', 'Сомони', 'с.'),
        ('TMT', 'Манат (новый туркменский манат)', 'm'),
        ('TND', 'Динар (тунисский динар)', '.د.ت • TD'),
        ('TOP', 'Паанга[30]', '$'),
        ('TRY', 'Лира (турецкая лира)', '₺'),
        ('TTD', 'Доллар (доллар Тринидада и Тобаго)', '$'),
        ('TWD', 'Доллар (новый тайваньский доллар)', 'NT$'),
        ('TZS', 'Шиллинг (танзанийский шиллинг)', 'TSh'),
        ('UAH', 'Гривна', '₴'),
        ('UGX', 'Шиллинг (угандийский шиллинг)', 'USh'),
        ('USD', 'Доллар (доллар США)', '$'),
        ('UYU', 'Песо (уругвайское песо)', '$'),
        ('UZS', 'Сум (узбекский сум)', 'so’m • сўм'),
        ('VES', 'Суверенный боливар', 'Bs. S.'),
        ('VND', 'Донг', '₫'),
        ('VUV', 'Вату', 'Vt'),
        ('WST', 'Тала', '$'),
        ('XAF', 'Франк (франк КФА BEAC)', '₣'),
        ('XCD', 'Доллар (восточно-карибский доллар)', '$'),
        ('XOF', 'Франк (франк КФА BCEAO)', '₣'),
        ('XPF', 'Франк (франк КФП)', '₣'),
        ('YER', 'Риал (йеменский риал)', '﷼ • YR'),
        ('ZAR', 'Рэнд', 'R'),
        ('ZMW', 'Квача (замбийская квача)', 'K'),
        ('ZWL', 'Доллар (доллар Зимбабве)', 'Z$');";

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialCurrencies");
        }
    }
}