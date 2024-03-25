import bb, {line, bar, areaSpline, pie, zoom} from "billboard.js";

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
    
    console.log(percents, counts, titles);
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
                    text: 'Количество',
                    position: 'outer-middle',
                }
            },
            y2: {
                show: true,
                label: {
                    text: 'Сумма',
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