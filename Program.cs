using System.Collections;

#region First task
Console.WriteLine("Первое задание");
Console.WriteLine("Введите кол-во строк матрицы");
int stroka = int.Parse(Console.ReadLine());
Console.WriteLine("Введите кол-во столбцов матрицы");
int stolbec = int.Parse(Console.ReadLine());
Console.WriteLine("Введите нижнюю границу диапазона значений");
int minValue = int.Parse(Console.ReadLine());
Console.WriteLine("Введите верхнюю границу диапазона значений");
int maxValue = int.Parse(Console.ReadLine());

MyMatrix first_matrix = new MyMatrix(stroka, stolbec, minValue, maxValue);
MyMatrix second_matrix = new MyMatrix(stroka, stolbec, minValue, maxValue);


Console.WriteLine("Матрица 1");
Console.WriteLine(first_matrix);
Console.WriteLine("Матрица 2");
Console.WriteLine(second_matrix);

//перегрузка +
MyMatrix Sum = first_matrix + second_matrix;
Console.WriteLine("Сумма матриц 1+2");
Console.WriteLine(Sum);

//перегрузка -
MyMatrix Raznost = first_matrix - second_matrix;
Console.WriteLine("Разность матриц 1-2");
Console.WriteLine(Raznost);

//перегрузка * для матриц
MyMatrix multiply = first_matrix * second_matrix;
Console.WriteLine("Произведение матриц 1*2");
Console.WriteLine(multiply);

//перегрузка * для числа
Console.WriteLine("Умножение матриц на числа");
MyMatrix chislo_multiply_1 = first_matrix * 2;
Console.WriteLine("Первая матрица умноженная на 2");
Console.WriteLine(chislo_multiply_1);
MyMatrix chislo_multiply_2 = second_matrix * 3;
Console.WriteLine("Вторая матрица умноженная на 3");
Console.WriteLine(chislo_multiply_2);

//перегрузка / на число
Console.WriteLine("Деление матриц на числа (значения округлены)");
MyMatrix divide_1 = first_matrix / 2;
Console.WriteLine("Первая матрица поделенная на 2");
Console.WriteLine(divide_1);
MyMatrix divide_2 = second_matrix / 3;
Console.WriteLine("Вторая матрица поделенная на 3");
Console.WriteLine(divide_2);
#endregion

#region Second task
Console.WriteLine("");
Console.WriteLine("Второе задание");
Car[] cars = new Car[]
        {
            new Car("BMW",2023,250),
            new Car("MERCEDES",2024,230),
            new Car("AUDI",2021,200),
            new Car("FORD",2020,198),
        };


//вывод для массива
static void CarPrinter(Car[] cars)
{
    foreach (Car car in cars)
    {
        Console.WriteLine(car);
    }
}

Console.WriteLine("Неотсортированный массив");
CarPrinter(cars);

Console.WriteLine("Введите по какому полю будет производиться сортировка");
string Pole = Console.ReadLine();
switch (Pole)
{
    case "name":
        Array.Sort(cars, new CarComparer(CarComparer.SortPole.Name));
        Console.WriteLine("Сортировка по Названию");
        CarPrinter(cars);
        break;
    case "year":
        Array.Sort(cars, new CarComparer(CarComparer.SortPole.ProductionYear));
        Console.WriteLine("Сортировка по году выпуска");
        CarPrinter(cars);
        break;
    case "speed":
        Array.Sort(cars, new CarComparer(CarComparer.SortPole.MaxSpeed));
        Console.WriteLine("Сортировка по максимальной скорости");
        CarPrinter(cars);
        break;
}
#endregion

#region Third task
Console.WriteLine("");
Console.WriteLine("Третье задание");
cars = new Car[]
        {
            new Car("BMW",2024,250),
            new Car("MERCEDES",2009,230),
            new Car("AUDI",1998,200),
            new Car("FORD",2025,198),
        };

CarCatalog catalog = new CarCatalog(cars);

Console.WriteLine("Прямой проход");
foreach (Car car in catalog)
{
    Console.WriteLine(car);
}

Console.WriteLine("Обратный обход");
foreach (Car car in catalog.GetEnumeratorR())
{
    Console.WriteLine(car);
}
Console.WriteLine("Проход с фильтром по году выпуска");
Console.WriteLine("Введите год выпуска: ");
int year = int.Parse(Console.ReadLine());
foreach (Car car in catalog.ByProductionYear(year))
{
    Console.WriteLine(car);
}

Console.WriteLine("Проход с фильтром по максимальной скорости");
Console.WriteLine("Введите максимальную скорость");
int speed = int.Parse(Console.ReadLine());
foreach (Car car in catalog.ByMaxSpeed(speed))
{
    Console.WriteLine(car);
}
#endregion

#region First task
class MyMatrix
{
    public int[,] mx;
    public int Stolbec, Stroka;
    public Random rnd;

    public MyMatrix(int stolbec, int stroka, int minValue, int maxValue)
    {
        Stolbec = stolbec;
        Stroka = stroka;
        mx = new int[Stolbec, Stroka];
        rnd = new Random();

        for (int i = 0; i < Stroka; i++)
        {
            for (int j = 0; j < Stolbec; j++)
            {
                mx[i, j] = rnd.Next(minValue, maxValue + 1);
            }
        }
    }


    //индекс
    public int this[int i, int j]
    {
        get { return mx[i, j]; }
        set { mx[i, j] = value; }
    }

    //перегрузка +
    public static MyMatrix operator +(MyMatrix one, MyMatrix two)
    {
        if ((one.Stroka != two.Stroka) || (one.Stolbec != two.Stolbec))
        {
            throw new ArgumentException("Размеры матриц должны быть одинаковыми");
        }

        MyMatrix rez = new MyMatrix(one.Stroka, one.Stolbec, 0, 0);
        for (int i = 0; i < one.Stroka; i++)
        {
            for (int j = 0; j < two.Stolbec; j++)
            {
                rez[i, j] = one[i, j] + two[i, j];
            }
        }
        return rez;
    }
    //перегрузка -
    public static MyMatrix operator -(MyMatrix one, MyMatrix two)
    {
        if ((one.Stroka != two.Stroka) || (one.Stolbec != two.Stolbec))
        {
            throw new ArgumentException("Размеры матриц должны быть одинаковыми");
        }

        MyMatrix rez = new MyMatrix(one.Stroka, one.Stolbec, 0, 0);
        for (int i = 0; i < one.Stroka; i++)
        {
            for (int j = 0; j < two.Stolbec; j++)
            {
                rez[i, j] = one[i, j] - two[i, j];
            }
        }
        return rez;
    }
    //перегрузка * для матриц
    public static MyMatrix operator *(MyMatrix one, MyMatrix two)
    {
        if (one.Stolbec != two.Stroka)
        {
            throw new ArgumentException("Кол-во столбцов первой матрицы должно быть равно кол-ву строк второй матрицы");
        }

        MyMatrix rez = new MyMatrix(one.Stroka, two.Stolbec, 0, 0);
        for (int i = 0; i < one.Stroka; i++)
        {
            for (int j = 0; j < two.Stolbec; j++)
            {
                for (int k = 0; k < one.Stolbec; k++)
                {
                    rez[i, j] += one[i, k] * two[k, j];
                }
            }
        }
        return rez;
    }

    //перегрузка * для числа
    public static MyMatrix operator *(MyMatrix one, int multiply)
    {
        MyMatrix rez = new MyMatrix(one.Stroka, one.Stolbec, 0, 0);

        for (int i = 0; i < one.Stroka; i++)
        {
            for (int j = 0; j < one.Stolbec; j++)
            {
                rez[i, j] = one[i, j] * multiply;
            }
        }
        return rez;
    }

    //перегрузка деления /
    public static MyMatrix operator /(MyMatrix one, int multiply)
    {
        if (multiply == 0)
        {
            throw new DivideByZeroException("Деление на ноль");
        }

        MyMatrix rez = new MyMatrix(one.Stroka, one.Stolbec, 0, 0);
        for (int i = 0; i < one.Stroka; i++)
        {
            for (int j = 0; j < one.Stolbec; j++)
            {
                rez[i, j] = one[i, j] / multiply;
            }
        }
        return rez;
    }

    //перегрузка ToString() для вывода матрицы в консоль
    public override string ToString()
    {
        string rez = "";
        for (int i = 0; i < Stroka; i++)
        {
            for (int j = 0; j < Stolbec; j++)
            {
                rez += mx[i, j] + " ";
            }
            rez += "\n";
        }
        return rez;
    }
}
#endregion

#region Second task
class Car
{
    public string Name { get; set; }
    public int ProductionYear { get; set; }
    public int MaxSpeed { get; set; }
    public Car(string name, int productionYear, int maxSpeed)
    {
        Name = name;
        ProductionYear = productionYear;
        MaxSpeed = maxSpeed;
    }

    public override string ToString()
    {
        return $"{Name} {ProductionYear} {MaxSpeed}";
    }

}

class CarComparer : IComparer<Car>
{
    //распознать по какому полю сортировка
    public enum SortPole
    {
        Name, ProductionYear, MaxSpeed
    }

    public SortPole crit;
    public CarComparer(SortPole sortPole)
    {
        crit = sortPole;
    }


    public int Compare(Car x, Car y)
    {
        switch (crit)
        {
            case SortPole.Name:
                return string.Compare(x.Name, y.Name);
            case SortPole.ProductionYear:
                return x.ProductionYear.CompareTo(y.ProductionYear);
            case SortPole.MaxSpeed:
                return x.MaxSpeed.CompareTo(y.MaxSpeed);
            default:
                throw new ArgumentException("Неизвестный параметр сортировки");
        }
    }
}
#endregion

#region Third task

class CarCatalog : IEnumerable<Car>
{
    public Car[] cars;

    public CarCatalog(Car[] cars)
    {
        this.cars = cars;
    }


    // прямой обход
    public IEnumerator<Car> GetEnumerator()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            yield return cars[i];
        }
    }
    // обратный обход
    public IEnumerable<Car> GetEnumeratorR()
    {
        for (int i = cars.Length - 1; i >= 0; i--)
        {
            yield return cars[i];
        }
    }
    // обход с фильтром года
    public IEnumerable<Car> ByProductionYear(int year)
    {
        foreach (Car car in cars)
        {
            if (car.ProductionYear >= year)
            {
                yield return car;
            }
        }
    }

    // обход с филтром скорости
    public IEnumerable<Car> ByMaxSpeed(int maxSpeed)
    {
        foreach (Car car in cars)
        {
            if (car.MaxSpeed >= maxSpeed)
            {
                yield return car;
            }
        }
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return cars.GetEnumerator();
    }
}
#endregion
