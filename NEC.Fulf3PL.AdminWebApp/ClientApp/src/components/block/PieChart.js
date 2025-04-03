import React from 'react';
import { Chart as ChartJS, ArcElement, Tooltip, Legend, Title } from 'chart.js';
import { Pie } from 'react-chartjs-2';

ChartJS.register(ArcElement, Tooltip, Legend, Title);

const PieChart = ({ data = null, title = "Chart 1" }) => {
    if (!data?.values || !data?.labels) return '';

    const chartValues = data?.values && !data?.values.every(value => value === 0) ? data?.values : [0, 0, 0, 0];

    const defaultData = {
        labels: data.formattedLabels ?? ['Completed', 'Failed', 'Pending', 'Total'],
        datasets: [
            {
                label: 'Count',
                data: chartValues,
                backgroundColor: data.backgroundColor ?? [
                    'rgb(76, 255, 133, 0.322)',
                    'rgb(252, 209, 48, 0.502)',
                    'rgb(255, 43, 43, 0.4)',
                    'rgb(0, 195, 255, 0.349)',
                ],
                borderColor: data.borderColor ?? [
                    'rgb(12, 207, 4)',
                    'rgb(247, 178, 4)',
                    'rgb(252, 4, 4)',
                    'rgba(0, 172, 250)',
                ],
                borderWidth: 1,
            },
        ],
    };

    const chartData = defaultData;
    const formatTitle = (title) => {
        if (title) {
            return title.replace(/([a-z])([A-Z])/g, '$1 $2');
        }
        return title;
    };

    const options = {
        responsive: true,
        plugins: {
            title: {
                display: true,
                text: formatTitle(data.title)?.toUpperCase() ?? title,
                font: {
                    size: 18
                },
                padding: {
                    top: 10,
                    bottom: 10
                }
            },
            legend: {
                position: 'right',
                labels: {
                    usePointStyle: true
                }
            },
            datalabels: {
                display: true,
                formatter: (value, context) => {
                    return `${context.chart.data.labels[context.dataIndex]}: ${value}`;
                },
                align: 'end',
                anchor: 'end'
            },
        },
    };

    return (
        <div style={{ position: 'relative', width: '90%', margin: 'auto' }}>
            <Pie data={chartData} options={options} />
        </div>
    );
}

export default PieChart;
