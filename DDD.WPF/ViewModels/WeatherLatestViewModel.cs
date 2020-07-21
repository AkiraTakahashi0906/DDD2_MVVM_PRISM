using DDD.Domain.Entities;
using DDD.Domain.Exceptions;
using DDD.Domain.Repositories;
using DDD.Infrastructure.SQLite;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DDD.WPF.ViewModels
{
    public class WeatherLatestViewModel : ViewModelBase
    {

            private IWeatherRepository _weather;
        IAreasRepository _areasRepository;

        public WeatherLatestViewModel()
            : this(new WeatherSQLite(), new AreasSQLite())
        {
        }

        public WeatherLatestViewModel(
            IWeatherRepository weather,
            IAreasRepository areas)
        {
            _weather = weather;
            _areasRepository = areas;

            foreach (var area in _areasRepository.GetData())
            {
                Areas.Add(new AreaEntity(area.AreaId, area.AreaName));
            }

            LatestButton = new DelegateCommand(LatestButtonExecute);
            TestButton = new DelegateCommand(TestButtonExecute);
            TestButton2 = new DelegateCommand(TestButtonExecute2);
        }

        public DelegateCommand LatestButton { get; }
        public DelegateCommand TestButton { get; }
        public DelegateCommand TestButton2 { get; }

        private string _bindText = string.Empty;
        public string BindText
        {
            get { return _bindText; }
            set
            {
                SetProperty(ref _bindText, value);
            }
        }

        private string _bindText2 = string.Empty;
        public string BindText2
        {
            get { return _bindText2; }
            set
            {
                SetProperty(ref _bindText2, value);
            }
        }

        //コンボボックスにバインドするやつ
        private ObservableCollection<AreaEntity> _areas = new ObservableCollection<AreaEntity>();
        public ObservableCollection<AreaEntity> Areas
        {
            get { return _areas; }
            set
            {
                SetProperty(ref _areas, value);
            }
        }

        private AreaEntity _selectedArea;
        public AreaEntity SelectedArea
        {
            get { return _selectedArea; }
            set
            {
                SetProperty(ref _selectedArea, value);
            }
        }

        private string _dataDateText = string.Empty;
        public string DataDateText
        {
            get { return _dataDateText; }
            set
            {
                SetProperty(ref _dataDateText, value);
            }
        }

        private string _conditionText = string.Empty;
        public string ConditionText
        {
            get { return _conditionText; }
            set
            {
                SetProperty(ref _conditionText, value);
            }
        }

        private string _temperatureText = string.Empty;
        public string TemperatureText
        {
            get { return _temperatureText; }
            set
            {
                SetProperty(ref _temperatureText, value);
            }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                SetProperty(ref _isSelected, value);
            }
        }

        private bool _isSelected2 = false;
        public bool IsSelected2
        {
            get { return _isSelected2; }
            set
            {
                SetProperty(ref _isSelected2, value);
            }
        }

        private void TestButtonExecute()
        {
            MessageBox.Show(BindText);
            BindText2 = "";
            IsSelected2 = false;
            IsSelected2 = true;
        }

        private void TestButtonExecute2()
        {
            MessageBox.Show(BindText2);
            BindText = "";
            IsSelected = false;
            IsSelected = true;
        }

        private void LatestButtonExecute()
        {
            if (SelectedArea == null)
            {
                throw new InputException("地域を選択してください");
            }

            var entity = _weather.GetLatest(SelectedArea.AreaId);
            if (entity == null)
            {
                DataDateText = string.Empty;
                ConditionText = string.Empty;
                TemperatureText = string.Empty;
            }
            else
            {
                DataDateText = entity.DataDate.ToString();
                ConditionText = entity.Condition.DisplayValue;
                TemperatureText = entity.Temperature.DisplayValueWithUnitSpace;
            }

            IsSelected = false;
            IsSelected = true;
        }
    }
}
