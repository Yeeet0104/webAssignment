<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="testingDb.aspx.cs" Inherits="webAssignment.testingDb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <title></title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <div style="width:600px">
                <canvas id="myChart" ></canvas>
            </div>
        </div>
    </form>
    <script>
        const ctx = document.getElementById('myChart').getContext('2d');
        const myChart = new Chart(ctx, {
            type: 'bar',
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
                datasets: [{
                    label: 'Monthly Sales',
                    data: [100000, 90000, 80000, 85000, 60000, 50000, 75000, 80000, 85000], // replace these values with your actual data
                    backgroundColor: [
                        '#6366F1',
                    ],
                    borderColor: [
                        '#6366F1',
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    y: {
                        beginAtZero: true,
                        grid: {
                            display: false
                        }
                    },
                    x: {
                        grid: {
                            display: false
                        },
                    }
                },
                plugins: {
                    legend: {
                        display: false
                    }
                },
            }
        });
    </script>
</body>
</html>
