using PIMM.Helpers;
using PIMM.Models;
using PIMM.Persistance;
using PIMM.Views.CategoriesDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace PIMM.ViewModels
{
    public class CategoriesViewModel : INotifyPropertyChanged
    {
        private Func<CategoryDto, bool> filter;
        public List<CategoryDto> categories;
        private CategoryDto selectedCategory;
        private readonly IPageService _pageService;
        private readonly IRepository _repository;
        private bool isRefreshing;
        

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand RefreshCommand { get; set; }
        public ICommand SelectCategoryCommand { get; private set; }
        public ICommand SearchCommand { get; private set; }
        public ICommand DeleteActionCommand { get; private set; }
        public ICommand EditActionCommand { get; private set; }
        public ICommand NewCategoryCommand { get; private set; }

        public ICommand IncomeSelectedCommand { get; set; }
        public ICommand ExpenseSelectedCommand { get; set; }

        public CategoriesViewModel(List<CategoryDto> categories, IPageService pageService, IRepository repository)
        {
            this.Categories = categories;
            SelectedType = TransactionType.Expense;
            this.filter = (x) => { return x.Type == selectedType; };
            _pageService = pageService;
            this._repository = repository;
            RefreshCommand = new Command(CmdRefresh);
            SelectCategoryCommand = new Command<CategoryDto>(async vm => await SelectCategory(vm));
            DeleteActionCommand = new Command<CategoryDto>(async vm => await DeleteAction(vm));
            EditActionCommand = new Command<CategoryDto>(async vm => await EditAction(vm));
            NewCategoryCommand = new Command<CategoryDto>(async vm => await NewAction());
            SearchCommand = new Command<string>(s => Search(s));
            IncomeSelectedCommand = new Command(IncomeSelected);
            ExpenseSelectedCommand = new Command(ExpenseSelected);
            MessagingCenter.Subscribe<CategoriesDetailsPage, CategoryDto>(this, "UpdateCategory", async (page, vm) => { await UpdateCategory(page, vm); });
        }

        private TransactionType selectedType;

        public TransactionType SelectedType
        {
            get { return selectedType; }
            set { selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
                OnPropertyChanged(nameof(IsIncome));
                OnPropertyChanged(nameof(IsExpense));
                OnPropertyChanged(nameof(Categories));
            }
        }

        public bool IsIncome
        {
            get { return SelectedType==TransactionType.Income; }
            
        }

        public bool IsExpense
        {
            get { return SelectedType == TransactionType.Expense; }

        }


        private void ExpenseSelected()
        {
            SelectedType = TransactionType.Expense;
        }

        private void IncomeSelected()
        {
            SelectedType = TransactionType.Income;
        }


        private async Task NewAction()
        {
            var vm = new CategoryDto()
            {
                Color = "#ffffff",
                Description = "Generic Category"
            };
            await _pageService.PushAsync(new CategoriesDetailsPage(_pageService, _repository, vm));
        }

        private async Task UpdateCategory(CategoriesDetailsPage page, CategoryDto vm)
        {
            await _pageService.PopAsync();
            _repository.UpdateCategory(vm);
            MessagingCenter.Send(this, "RefreshCategory");
        }

        private async Task EditAction(CategoryDto vm)
        {
            await _pageService.PushAsync(new CategoriesDetailsPage(_pageService, _repository, vm));
        }

        private async Task DeleteAction(CategoryDto vm)
        {
            var deleteConfirmation = await _pageService.DisplayAlert("Delete category", "Are you sure?", "Yes", "No");
            var category = new Mapping().CategoryDto_2_Category(vm);
            if (deleteConfirmation)
            {
                var status = _repository.DeleteCategory(category);
                if (status != "OK")
                {
                    await _pageService.DisplayAlert("Erase canceled", status, "OK");
                }
                else
                {
                    MessagingCenter.Send(this, "DeleteCategories");
                }
            }
        }

        private void Search(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                this.filter = (x) => { return  x.Type == selectedType; };
            }
            else
            {
                this.filter = (x) => {
                    return ((x.Description.ToLower().Contains(s.ToLower())) && x.Type ==selectedType);
                };
            }
            OnPropertyChanged(nameof(Categories));
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { isRefreshing = value; OnPropertyChanged(nameof(IsRefreshing)); }
        }

        public CategoryDto SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public List<CategoryDto> Categories
        {
            get
            {
                return categories.Where(filter).ToList();
            }
            set
            {
                categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        private async Task SelectCategory(CategoryDto category)
        {
            SelectedCategory = null;
        }

        private void CmdRefresh()
        {
            IsRefreshing = true;
            MessagingCenter.Send(this, "RefreshCategory");
            IsRefreshing = false;
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }
    }
}
