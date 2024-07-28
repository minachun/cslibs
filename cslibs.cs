using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cslibs
{
    /// <summary>
    /// 手抜き（クラス）ライブラリ集
    /// </summary>

    
    public class UIBackTask<Targ, Tresult, Tnotice>
    {
        // UIでのバックグラウンドで動かすタスクのヘルパー
        // 時間のかかる処理を、UIの裏で動かして、タスク立ち上げ、キャンセル、進捗通知、など必要な処理を定型的に実装するためのインターフェース

        private Task<Tresult>? backtask;
        private CancellationTokenSource? cancel_backtask;
        private Form targetForm;

        public UIBackTask(Form _f)
        {
            this.backtask = null;
            this.cancel_backtask = null;
            this.targetForm = _f;
            this._noticefunc = null;
            this._noticetime = new Stopwatch();
        }

        /// <summary>
        /// バックグラウンドタスクが終わるまで待つ
        /// </summary>
        /// <param name="isFinished">UIでアプリをClose()するタイミングで呼び出す場合はtrue</param>
        public void CanelAndWait(bool isFinished)
        {
            this.cancel_backtask?.Cancel();
            if (isFinished)
            {
                while (this.backtask?.Wait(1) == false)
                {
                    // UIを最後回す(これやらないと終わる直前にInvokeでUI側を呼び出すケースで終われなくなるため)
                    Application.DoEvents();
                }
            }
            else
            {
                // UIなしなので待つだけ
                this.backtask?.Wait();
            }
        }

        private Action<Tnotice>? _noticefunc;
        private Stopwatch _noticetime;
        private Queue<Tnotice> nbuffer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private bool NotifyFunc(Tnotice n)
        {
            // キャンセルチェック
            if ( this.cancel_backtask != null)
            {
                CancellationToken _t = this.cancel_backtask.Token;
                if (_t.IsCancellationRequested)
                {
                    // キャンセルリクエストがあったら直ぐに終わる
                    return true;
                }
            }

            // 沢山送りすぎるとまずいので、2msec.待つ
            if ( this._noticetime.ElapsedMilliseconds >= 2 )
            {
                this._noticetime.Restart();
                if (this._noticefunc != null)
                {
                    this.nbuffer.Enqueue(n);
                    int cnt = 0;
                    while (this.nbuffer.Count > 0)
                    {
                        var _n = this.nbuffer.Dequeue();
                        this.targetForm.Invoke((Action)(() => this._noticefunc(_n)));
                        cnt++;
                        if ( cnt >= 30 )
                        {
                            // 30個で様子見
                            break;
                        }
                    }
                }
            } else
            {
                // メッセージがいっぱいで送れなかった
                if (this._noticefunc != null)
                {
                    this.nbuffer.Enqueue(n);
                }
            }

            return false;

        }

        public Tresult Result {
            get {
                return this.backtask.Result;
            }
        }


        /// <summary>
        /// バックグラウンドタスクの実行開始
        /// </summary>
        /// <param name="execfunc">バックグラウンドタスク Tresult execfunc(Targ _arg, Func<Tnotice,bool> _nfunc) _nfunc は息継ぎ・通知関数です </param>
        /// <param name="_arg">バックグラウンドタスク実行時に渡す引数</param>
        /// <param name="noticefunc">UI通知関数 void noticefunc(Tnotice n)</param>
        public void Execute(Func<Targ, Func<Tnotice, bool>, Tresult> execfunc, Targ _arg, Action<Tnotice> noticefunc)
        {
            this.cancel_backtask = new CancellationTokenSource();
            this._noticefunc = noticefunc;
            this._noticetime.Restart();
            this.nbuffer = new Queue<Tnotice>();
            this.backtask = Task.Factory.StartNew(() => {
                var _r = execfunc(_arg, this.NotifyFunc);
                // 通知を全部送りきる
                foreach ( var _n in this.nbuffer)
                {
                    this.targetForm.Invoke((Action)(() => this._noticefunc(_n)));
                }
                return _r;
            });
        }


    }

}
