using System.ComponentModel;

namespace Metalama.Documentation.SampleCode.AspectFramework.IntroducePropertyChanged3
{
    [IntroducePropertyChangedAspect]
    class TargetCode
    {


        protected void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
