using System.Diagnostics.Eventing.Reader;

namespace cslibs
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this._task = null;
            InitializeComponent();
        }

        private UIBackTask<string, bool, string>? _task;


        private void UIBackTaskNotify(string _v)
        {
            this.tbUIBackTask.Text += System.Environment.NewLine + _v;
        }

        private bool UIBackTaskFunc(string _arg, Func<string, bool> _notify)
        {
            // テストタスク
            for ( int _i = 0; _i < 1000; _i++ )
            {
                // キャンセルされたら抜ける
                if (_notify($"test {_i:d}")) break;
                Thread.Sleep(10);
            }
            return false;
        }

        private void btnTestUIBackTask_Click(object sender, EventArgs e)
        {
            // UIBackTaskクラスのテスト
            if (this._task != null)
            {
                // 停止
                this._task?.CanelAndWait(false);
                this.btnUIBackTask.Text = "UIBackTestのテスト";
                this._task = null;
            }
            else
            {
                // 開始
                this._task = new UIBackTask<string, bool, string>(this);

                this._task.Execute(this.UIBackTaskFunc, "Test", this.UIBackTaskNotify);

                this.btnUIBackTask.Text = "Cancel";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // タスクを終了させる
            this._task?.CanelAndWait(true);
        }
    }
}
