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
    class ViewModelLostSoul : INotifyPropertyChanged
    {
        DB DB;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<LostSoul> LostSouls { get; set; }
        void SignalChanged([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        public Commands AddLostSoul { get; set; }
        public Commands SaveLostSoul { get; set; }
        public Commands DeleteLostSoul { get; set; }
        public ViewModelLostSoul() /*Кантсруктар*/
        {
            DB = DB.GetDb();
            LostSouls = new ObservableCollection<LostSoul>(DB.LostSouls);
            SignalChanged("LostSouls");

            #region Команда с добавлением(Кнопка)
            AddLostSoul = new Commands(() =>
            {
                var lostsoul = new LostSoul {/* Name = "", FatherName = "", Surname = "", Telephone = "", Email = "" */};
                DB.LostSouls.Add(lostsoul);
                SelectedLostSoul = lostsoul;

            });
            #endregion

            #region Команда с сохранением(Кнопка)
            SaveLostSoul = new Commands(() =>
            {
                try
                {
                    DB.SaveChanges();
                    LoadLostSouls();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });
            #endregion

            #region Команда с удалением(Кнопка)
            DeleteLostSoul = new Commands(() =>
            {
                DB.LostSouls.Remove(selectedLostSoul);
                DB.SaveChanges();
                LoadLostSouls();
            });
        }
            #endregion

        private LostSoul selectedLostSoul;

        public LostSoul SelectedLostSoul
        {
            get => selectedLostSoul;
            set
            {
                selectedLostSoul = value;
                SignalChanged();
            }
        }

        private void LoadLostSouls()
        {
            LostSouls = new ObservableCollection<LostSoul>(DB.LostSouls);
            SignalChanged("LostSouls");
        }
    }
}

