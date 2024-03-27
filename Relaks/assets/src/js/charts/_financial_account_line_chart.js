import bb, {line, bar, areaSpline, pie, zoom} from "billboard.js";

function nFormatter(num, digits) {
    const lookup = [
        { value: 1, symbol: "" },
        { value: 1e3, symbol: "k" },
        { value: 1e6, symbol: "M" },
        { value: 1e9, symbol: "G" },
        { value: 1e12, symbol: "T" },
        { value: 1e15, symbol: "P" },
        { value: 1e18, symbol: "E" }
    ];
    const regexp = /\.0+$|(?<=\.[0-9]*[1-9])0+$/;
    const item = lookup.findLast(item => num >= item.value);
    return item ? (num / item.value).toFixed(digits).replace(regexp, "").concat(item.symbol) : "0";
}

window.InitializeFinancialPercentageChart = async (data) => {
    const json = JSON.parse(data);
    if (!json) throw new Error('chart data model not found');
    const chartEl = document.getElementById(json.HtmlElementId);
    const currencyId = json.Data.CurrencyId;
    
    const percents = ['Проценты'];
    const counts = ['Количество'];
    const totalIncome = ['Пополнения'];
    const totalOutcome = ['Списания'];
    const titles = ['x'];
    json.Data.CategoryItems.forEach(item => {
        
        titles.push(item.IsNotUsedDirectly ? item.Title : `* ${item.Title}`);
        percents.push(item.Percent);
        counts.push(item.Count);
        totalIncome.push(item.TotalIncome);
        totalOutcome.push(Math.abs(item.TotalOutcome));
    });
    
    // console.log(percents, counts, titles);
    bb.generate({
        data: {
            x: 'x',
            columns: [
                titles,
                percents,
                counts,
                totalIncome,
                totalOutcome,
            ],
            types: {
                [percents[0]]: line(),
                [counts[0]]: bar(),
                [totalIncome[0]]: bar(),
                [totalOutcome[0]]: bar(),
            },
            axes: {
                [totalIncome[0]]: 'y2',
                [totalOutcome[0]]: 'y2',
            }
        },
        axis: {
            rotated: true,
            x: {
                type: 'category',
                tick: {
                    multiline: false,
                    tooltip: true,
                    format: function(index, categoryName) {
                        return categoryName.substring(0, 25);
                    },
                }
            },
            y: {
                label: {
                    text: 'Количество, шт.',
                    position: 'outer-middle',
                }
            },
            y2: {
                show: true,
                tick: {
                    format: function(x) {
                        
                        return nFormatter(x, 2);
                    }
                },
                label: {
                    text: 'Сумма, ' + currencyId,
                    position: 'outer-middle',
                },
            },
        },
        tooltip: {
            format: {
                value: function(value, ratio, id) {
                    // var format = id === "data1" ? d3.format(',') : d3.format('$');
                    if (id === percents[0]) {
                        return value.toFixed(2) + '%';
                    } else if (id === totalIncome[0] || id === totalOutcome[0]) {
                        return new Intl.NumberFormat('ru-RU', {style: 'currency', currency: currencyId}).format(value);
                    }

                    return value;
                }
            }
        },
        // legend: {
        //     position: "right"
        // },
        bindto: chartEl
    });
}

window.InitializeFinancialLineChart = async (data) => {
    const json = JSON.parse(data);
    if (!json) throw new Error('chart data model not found');
    const chartEl1 = document.getElementById(json.HtmlElementIds[0]);
    const chartEl2 = document.getElementById(json.HtmlElementIds[1]);
    
    const dates = json.Dates;
    const axisConfig = {
        x: {
            type: "timeseries",
            tick: {
                format: json.DatesFormat,
            }
        },
        y: {
            tick: {
                format: function(val) {
                    return new Intl.NumberFormat('ru-RU').format(val);
                }
            }
        }
    };

    const firstTitle = Object.keys(json.Data.First)[0];
    bb.generate({
        boost: {
            useCssRule: true
        },
        data: {
            x: "x",
            json: {
              x: dates,
              ...json.Data.First,
            },
            types: {
                [firstTitle]: areaSpline(),
            },
            type: line(),
        },
        axis: axisConfig,
        tooltip: {
            linked: true,
            format: {
                value: function(val) {
                    return new Intl.NumberFormat('ru-RU', { style: 'currency', currency: json.Currency }).format(val)
                }
            }
        },
        zoom: {
            enabled: zoom(), // for ESM specify as: zoom()
        },
        bindto: chartEl1,
    });


    const secondTypes = {};
    json.Data.SecondBarGroups.forEach(group => {
        group.forEach(title => {
            secondTypes[title] = bar();
        });
    })
    
    bb.generate({
        boost: {
            useCssRule: true
        },
        data: {
            x: "x",
            json: {
                x: dates,
                ...json.Data.Second,
            },
            groups: json.Data.SecondBarGroups,
            types: secondTypes,
            type: areaSpline(),
        },
        axis: axisConfig,
        tooltip: {
            linked: true,
            format: {
                value: function(val) {
                    return new Intl.NumberFormat('ru-RU', { style: 'currency', currency: json.Currency }).format(val)
                }
            }
        },
        zoom: {
            enabled: zoom(),
        },
        bindto: chartEl2,
    });
}