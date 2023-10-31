using System;

namespace mod3chap1task1
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource cts;
        private List<ProgressBarData> progressBars;
        Random random = new Random();

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            progressBars = new List<ProgressBarData>();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            cts = new CancellationTokenSource();
            Random random = new Random();

            foreach (ProgressBarData progressBar in progressBars)
            {
                progressBar.CurrentValue = 0;
                progressBar.IsActive = true;

                Task.Run(() =>
                {
                    while (progressBar.CurrentValue < progressBar.MaxValue && !cts.IsCancellationRequested)
                    {
                        int increment = random.Next(1, 10);
                        progressBar.CurrentValue += increment;

                        if (progressBar.CurrentValue > progressBar.MaxValue)
                        {
                            progressBar.CurrentValue = progressBar.MaxValue;
                        }

                        UpdateProgressBar(progressBar);
                        Thread.Sleep(100);
                    }

                    progressBar.IsActive = false;
                }, cts.Token);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int maxValue = random.Next(10, 100);
            Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            ProgressBarData progressBar = new ProgressBarData(maxValue, color);
            progressBars.Add(progressBar);

            AddProgressBar(progressBar);
        }

        private void AddProgressBar(ProgressBarData progressBar)
        {
            ProgressBar pb = new ProgressBar();
            pb.Maximum = progressBar.MaxValue;
            pb.Value = progressBar.CurrentValue;
            pb.Style = ProgressBarStyle.Continuous;
            pb.ForeColor = progressBar.Color;

            flowLayoutPanel.Controls.Add(pb);
        }

        private void UpdateProgressBar(ProgressBarData progressBar)
        {
            ProgressBar pb = flowLayoutPanel.Controls[progressBars.IndexOf(progressBar)] as ProgressBar;
            pb.Value = progressBar.CurrentValue;
            pb.ForeColor = progressBar.Color;
        }
    }
}