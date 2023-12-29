import bb, {line, zoom} from "billboard.js";

window.InitializeFinancialChart = (data) => {
    const model = JSON.parse(data);
    if (!model) throw new Error('chart data model not found');
    const chartEl = document.getElementById(model.htmlElementId);
    if (!chartEl) throw new Error('js chart id #js-line-chart not found')
    
    let columns = [];
    model.accounts.forEach(account => {
        // console.log(account.totalIncome, account.totalOutlay);
        // console.log(account.totalIncome2, account.totalOutlay2);
        // console.log(account.items);
        let avgBalance = ['Средний баланс'];
        // let totalIncome = ['Пополнения'];
        // let totalOutlay = ['Списания'];
        
        account.items.forEach(item => {
            avgBalance.push(item.averageBalance);
            // totalIncome.push(item.totalIncome);
            // totalOutlay.push(item.totalOutlay);
        })
        
       columns.push(avgBalance)
    });

    
    const chart = bb.generate({
        bindto: chartEl,
        data: {
            type: line(),
            columns: columns,
            // columns: [
            //     ["data1", 30, 200, 100, 400, 150, 250]
            // ]
        },
        zoom: {
            enabled: zoom()
        }
    });
    // setTimeout(function() {
    //     chart.load({
    //         columns: [
    //             ["data3", 130, 150, 200, 300, 200, 100]
    //         ]
    //     });
    // }, 1500);
    //
    // setTimeout(function() {
    //     chart.unload({
    //         ids: "data1"
    //     });
    // }, 2000);
}