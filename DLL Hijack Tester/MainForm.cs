using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DllHijackTesterApp
{
    public partial class MainForm : Form
    {
        private string lastExePath = null;
        private string tempFakeDllFolder = null;

        public MainForm()
        {
            InitializeComponent();
            // Set dark theme colors for controls on load
            SetupDarkTheme();
        }

        private void SetupDarkTheme()
        {
            this.BackColor = System.Drawing.Color.Black;
            this.ForeColor = System.Drawing.Color.Lime;

            txtExePath.BackColor = System.Drawing.Color.Black;
            txtExePath.ForeColor = System.Drawing.Color.Lime;

            lstResults.BackColor = System.Drawing.Color.Black;
            lstResults.ForeColor = System.Drawing.Color.Lime;
            lstResults.DrawMode = DrawMode.OwnerDrawFixed;
            lstResults.DrawItem += lstResults_DrawItem;

            btnBrowse.BackColor = System.Drawing.Color.Black;
            btnBrowse.ForeColor = System.Drawing.Color.Lime;
            btnBrowse.FlatStyle = FlatStyle.Flat;
            btnBrowse.FlatAppearance.BorderColor = System.Drawing.Color.Lime;

            btnTest.BackColor = System.Drawing.Color.Black;
            btnTest.ForeColor = System.Drawing.Color.Lime;
            btnTest.FlatStyle = FlatStyle.Flat;
            btnTest.FlatAppearance.BorderColor = System.Drawing.Color.Lime;

            btnSaveReport.BackColor = System.Drawing.Color.Black;
            btnSaveReport.ForeColor = System.Drawing.Color.Lime;
            btnSaveReport.FlatStyle = FlatStyle.Flat;
            btnSaveReport.FlatAppearance.BorderColor = System.Drawing.Color.Lime;

            // Hover effects
            btnBrowse.MouseEnter += Btn_MouseEnter;
            btnBrowse.MouseLeave += Btn_MouseLeave;
            btnTest.MouseEnter += Btn_MouseEnter;
            btnTest.MouseLeave += Btn_MouseLeave;
            btnSaveReport.MouseEnter += Btn_MouseEnter;
            btnSaveReport.MouseLeave += Btn_MouseLeave;
        }

        private void Btn_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn)
                btn.ForeColor = System.Drawing.Color.FromArgb(128, 255, 128);
        }

        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn)
                btn.ForeColor = System.Drawing.Color.Lime;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Executable Files (*.exe)|*.exe"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtExePath.Text = ofd.FileName;
                lstResults.Items.Clear();
            }
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            lstResults.Items.Clear();
            string exePath = txtExePath.Text;

            if (!File.Exists(exePath))
            {
                MessageBox.Show("Executable file not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lastExePath = exePath;

            try
            {
                // Step 1: Run normal scan
                lstResults.Items.Add("[*] Running normal DLL scan...");
                RunDllScan(exePath, null);

                // Step 2: Run scan with fake DLL injection
                lstResults.Items.Add("[*] Running scan with fake DLL injection...");

                SetupFakeDllFolder();

                RunDllScan(exePath, tempFakeDllFolder);

                CleanupFakeDllFolder();

                lstResults.Items.Add("[*] Scan completed.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RunDllScan(string exePath, string fakeDllFolder)
        {
            Process proc = new Process();

            proc.StartInfo.FileName = exePath;
            proc.StartInfo.UseShellExecute = false;

            if (fakeDllFolder != null)
            {
                // Prepend fakeDllFolder to PATH env variable
                string currentPath = Environment.GetEnvironmentVariable("PATH") ?? "";
                proc.StartInfo.EnvironmentVariables["PATH"] = fakeDllFolder + ";" + currentPath;
                proc.StartInfo.WorkingDirectory = fakeDllFolder;
            }

            proc.Start();

            System.Threading.Thread.Sleep(2500);

            foreach (ProcessModule module in proc.Modules)
            {
                string dllPath = module.FileName;
                string dllName = Path.GetFileName(dllPath);

                if (fakeDllFolder != null && dllPath.StartsWith(fakeDllFolder, StringComparison.OrdinalIgnoreCase))
                {
                    lstResults.Items.Add($"[HIJACKED] Loaded fake DLL: {dllName}");
                }
                else if (!File.Exists(dllPath))
                {
                    lstResults.Items.Add("[MISSING] " + dllPath);
                }
                else
                {
                    lstResults.Items.Add("[FOUND] " + dllPath);
                }
            }

            proc.Kill();
            proc.Dispose();
        }

        private void SetupFakeDllFolder()
        {
            tempFakeDllFolder = Path.Combine(Path.GetTempPath(), "FakeDllHijack_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFakeDllFolder);

            // Create a simple empty DLL file (can be replaced by a real dummy DLL)
            string fakeDllPath = Path.Combine(tempFakeDllFolder, "fake.dll");

            if (!File.Exists(fakeDllPath))
            {
                byte[] fakeDllBytes = GetMinimalDllBytes();
                File.WriteAllBytes(fakeDllPath, fakeDllBytes);
            }
        }

        private void CleanupFakeDllFolder()
        {
            try
            {
                if (tempFakeDllFolder != null && Directory.Exists(tempFakeDllFolder))
                {
                    Directory.Delete(tempFakeDllFolder, true);
                    tempFakeDllFolder = null;
                }
            }
            catch { /* ignore errors */ }
        }

        private byte[] GetMinimalDllBytes()
        {
            // Minimal valid DLL binary bytes that export DllMain and do nothing
            // This is a very basic precompiled DLL in raw bytes for demonstration.
            // For production, replace with your own compiled DLL or generate dynamically.

            return new byte[]
            {
                // Minimal PE header and stub (compiled dummy DLL)
                0x4D, 0x5A, 0x90, 0x00, // MZ header
                // ... (truncated for brevity, use your real DLL bytes here)
            };
        }

        private void lstResults_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0) return;

            string text = lstResults.Items[e.Index].ToString();

            e.DrawBackground();

            var color = System.Drawing.Color.Lime;
            if (text.StartsWith("[MISSING]"))
                color = System.Drawing.Color.Red;
            else if (text.StartsWith("[HIJACKED]"))
                color = System.Drawing.Color.Orange;

            using (var brush = new System.Drawing.SolidBrush(color))
            {
                e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
            }

            e.DrawFocusRectangle();
        }

        private void btnSaveReport_Click(object sender, EventArgs e)
        {
            if (lstResults.Items.Count == 0)
            {
                MessageBox.Show("No results to save.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string exePath = txtExePath.Text;
            string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            string defaultName = $"DLLHijackReport_{timestamp}.txt";

            SaveFileDialog sfd = new SaveFileDialog
            {
                FileName = defaultName,
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName))
                    {
                        sw.WriteLine("DLL Hijack Test Report");
                        sw.WriteLine("Executable: " + exePath);
                        sw.WriteLine("Timestamp: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        sw.WriteLine("====================================");
                        foreach (var item in lstResults.Items)
                        {
                            sw.WriteLine(item.ToString());
                        }
                    }

                    MessageBox.Show("Report saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to save report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
