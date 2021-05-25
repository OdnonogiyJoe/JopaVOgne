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
    public class ViewModelClient : INotifyPropertyChanged
    {
        DB DB;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Client> Clients { get; set; }
        void SignalChanged([CallerMemberName] string prop = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        #region Список мини-команд
        public Commands AddClient { get; set; }
        public Commands SaveClient { get; set; }
        public Commands DeleteClient { get; set; }
        #endregion

        #region Конструктор
        public ViewModelClient()
        {
            DB = DB.GetDb();
            Clients = new ObservableCollection<Client>(DB.Clients);

            #region Команда с добавлением(Кнопка)
            AddClient = new Commands(() =>
            {
                var client = new Client {/* Name = "",FatherName = "", Surname = "", Telephone = "", Email = "" */};
                DB.Clients.Add(client);
                SelectedClient = client;
            });
            #endregion

            #region Команда с сохранением(Кнопка)
            SaveClient = new Commands(() =>
            {
                try
                {
                    DB.SaveChanges();
                    LoadClients();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });
            #endregion

            #region Команда с удалением(Кнопка)
            DeleteClient = new Commands(() =>
            {
                DB.Clients.Remove(selectedClient);
                DB.SaveChanges();
                LoadClients();
            });
            #endregion

        }
        #endregion

        private Client selectedClient;

        public Client SelectedClient
        {
            get => selectedClient;
            set
            {
                selectedClient = value;
                SignalChanged();
            }
        }

        private void LoadClients()
        {
            Clients = new ObservableCollection<Client>(DB.Clients);
            SignalChanged("Clients");
        }
    }
}
