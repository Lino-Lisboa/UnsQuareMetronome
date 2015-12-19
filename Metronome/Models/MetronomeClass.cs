using System;
using System.Linq;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using System.Threading;
using Windows.UI.Core;

namespace Metronome.Models
{
    class MetronomeClass
    {
        //Fields        

        //Properties
        public int UpperNum { get; set; }
        public int LowerNum { get; set; }        
        public int Bpm { get; set; }
        public int TimerMSecs { get; set; }
        public System.Uri BaseURI { get; set; }
        public MediaElement Tick { get; set; }
        public MediaElement Tock { get; set; }
        public bool tickORtock { get; set; }
        public MainPage Page { get; set; }
        public Views.Composer ComposerPage { get; set; }
        public int CompassPickerComboBox { get; set; }
        public System.Threading.Timer myTimer { get; set; }
        public bool TimerCanceled { get; set; }
        public Stopwatch Watch { get; set; }

        //***************************************************************
        //Constructors
        public MetronomeClass(int upperNum, int lowerNum, int bpm, System.Uri baseURI, MainPage page, MediaElement mediaTick, MediaElement mediaTock)
        {
            UpperNum = upperNum;
            LowerNum = lowerNum;
            Bpm = bpm;
            BaseURI = baseURI;
            CalculateBpm(Bpm);
            Page = page;
            Tick = mediaTick;
            Tock = mediaTock;
            TimerCanceled = true;           
            Tick.AutoPlay = false;
            Tock.AutoPlay = false;
            Tick.Source = new Uri(Page.BaseUri, Page.Sounds.Where(X => X.Name == "Tick").FirstOrDefault().AudioFile.ToString());
            Tock.Source = new Uri(Page.BaseUri, Page.Sounds.Where(X => X.Name == "Click").FirstOrDefault().AudioFile.ToString());            
        }
        public MetronomeClass(int upperNum, int lowerNum, int bpm, System.Uri baseURI, Views.Composer composerPage, MediaElement mediaTick, MediaElement mediaTock)
        {
            UpperNum = upperNum;
            LowerNum = lowerNum;
            Bpm = bpm;
            BaseURI = baseURI;
            CalculateBpm(Bpm);
            ComposerPage = composerPage;
            Tick = mediaTick;
            Tock = mediaTock;            
            Tick.AutoPlay = false;
            Tock.AutoPlay = false;
            Tick.Source = new Uri(ComposerPage.BaseUri, ComposerPage.Sounds.Where(X => X.Name == "Tick").FirstOrDefault().AudioFile.ToString());
            Tock.Source = new Uri(ComposerPage.BaseUri, ComposerPage.Sounds.Where(X => X.Name == "Click").FirstOrDefault().AudioFile.ToString());
        }

        //*************************************************************
        //Methods 
        public void CalculateBpm(int bpms)
        {            
            this.TimerMSecs = (1000 * 60) / bpms;            
        }




        //*********************************
        //Timers Methods and Events        
        public void TimerSetUp()
        {
            myTimer = new System.Threading.Timer(StartInterval, null, TimerMSecs, Timeout.Infinite);
        }
        public async void StartInterval(object state)
        {
            var watch = new Stopwatch();

            if (!TimerCanceled)
            {
                await Page.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    watch.Start();
                    Tick.Play();
                    myTimer.Change(Math.Max(0, TimerMSecs - (int)watch.ElapsedMilliseconds), TimerMSecs - (int)watch.ElapsedMilliseconds);
                });
            }
            else
            {
                myTimer.Dispose();
                watch.Stop();
                watch.Reset();
                watch = null;
            }
            Watch = watch;
        }

    }        
    
}
