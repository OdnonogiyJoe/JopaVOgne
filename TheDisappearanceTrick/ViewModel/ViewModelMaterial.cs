using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TheDisappearanceTrick.Entities;

namespace TheDisappearanceTrick.ViewModel
{
    class ViewModelMaterial : INotifyPropertyChanged
    {
        DB DB;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Material> Materials { get; set; }
        void SignalChanged([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public ViewModelMaterial()
        {
            DB = DB.GetDb();
            Materials = new ObservableCollection<Material>(DB.Materials);
            SignalChanged("Materials");
        }

        private Material selectedMaterial;

        public Material SelectedMaterial
        {
            get => selectedMaterial;
            set
            {
                selectedMaterial = value;
                SignalChanged();
            }
        }
    }
}
