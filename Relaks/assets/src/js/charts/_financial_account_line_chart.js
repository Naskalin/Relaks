import bb, {line, bar} from "billboard.js";

window.InitializeFinancialChart = async (data) => {
    const json = JSON.parse(data);
    console.log(json);
    if (!json) throw new Error('chart data model not found');
    const chartEl1 = document.getElementById(json.HtmlElementIds[0]);
    const chartEl2 = document.getElementById(json.HtmlElementIds[1]);
    // if (!chartEl1 || !chartEl2) throw new Error(`js chart id ${chartEl1} or ${chartEl2} not found`)
    console.log(chartEl1, chartEl2);

    const dates = json.Dates;
    
    bb.generate({
        data: {
            x: "x",
            json: {
              x: dates,
              ...json.Data.First,
            },
            // columns: [
            //     // ['x', ...dates],
            //     // ["x", "2013-01-01", "2013-01-02", "2013-01-03", "2013-01-04", "2013-01-05", "2013-01-06", "2013-01-07", "2013-01-08", "2013-01-09", "2013-01-10", "2013-01-11", "2013-01-12"],
            //     // ["data", 2000000, 3000000, 100050, 1000000, 3000000, 4000000, 2000000, 3000000, 1000000, 1000000, 3000000, 4000000],
            // ],
            type: line(), // for ESM specify as: line(),
        },
        axis: {
            x: {
                type: "timeseries",
                tick: {
                    format: json.DatesFormat
                }
            }
        },
        tooltip: {
            linked: true
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
        data: {
            x: "x",
            json: {
                x: dates,
                ...json.Data.Second,
            },
            groups: json.Data.SecondBarGroups,
            types: secondTypes,
        },
        axis: {
            x: {
                type: "timeseries",
                tick: {
                    format: json.DatesFormat
                }
            }
        },
        tooltip: {
            linked: true
        },
        bindto: chartEl2,
    });
}