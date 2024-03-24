import bb, {line, bar, zoom} from "billboard.js";

window.InitializeFinancialChart = async (data) => {
    const json = JSON.parse(data);
    // console.log(json);
    if (!json) throw new Error('chart data model not found');
    const chartEl1 = document.getElementById(json.HtmlElementIds[0]);
    const chartEl2 = document.getElementById(json.HtmlElementIds[1]);
    // if (!chartEl1 || !chartEl2) throw new Error(`js chart id ${chartEl1} or ${chartEl2} not found`)
    // console.log(chartEl1, chartEl2);
    
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
        bindto: chartEl2,
    });
}