import bb, {line, bar, areaSpline, pie, zoom} from "billboard.js";

window.InitializeFinancialPercentageChart = async (data) => {
    const json = JSON.parse(data);
    if (!json) throw new Error('chart data model not found');
    const chartEl = document.getElementById(json.HtmlElementId);
    const columns = [];
    Object.keys(json.Data).forEach(title => {
        columns.push([title, json.Data[title]]); 
    });
    bb.generate({
        data: {
            columns,
            type: pie(),
        },
        legend: {
            position: "right"
        },
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
            enabled: zoom(), // for ESM specify as: zoom()
        },
        bindto: chartEl2,
    });
}