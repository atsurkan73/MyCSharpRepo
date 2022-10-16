/*
Task:

Implement 3 sort algorithms:

Selection
Bubble
Insertion

Extra:

Define enum SortAlgorithmType with all 3 algorithms types and create single function Sort that will accept array and SortAlgorithmType and use passed algorithm to sort array

Define enum OrderBy with 2 values: Asc and Desc and update Sort method with this parameter that will change sort order
*/




int[] numArray = new[] {12, 22, 98, 20, 78, 556, -4, 0, -2}; //Setting array to be sorted

// Invoking each sorting method separately

selectionSort(numArray);
bubbleSort(numArray);
insertionSort(numArray);

//Sorting with Acs order as default
sort(SortAlgorithmType.bubbleSort, numArray);

//Sorting with ability to define order type Asc or Desk. Order types are implemented in printArray(), printArrayDesc methods, respectively. 
sort(SortAlgorithmType.insertSort, numArray, AscOrderBy.Desc);

//Sorting with ability to define order type Asc or Desk. Desk order type is implemented with revertArray() method. 
sortAndRevert(SortAlgorithmType.selectionSort, numArray, AscOrderBy.Desc);

static int[] selectionSort (int[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n - 1; i++)
    {
        int min_idx = i;
        for (int j = i + 1; j < n; j++)
            if (arr[j] < arr[min_idx])
                min_idx = j;

        int temp = arr[min_idx];
        arr[min_idx] = arr[i];
        arr[i] = temp;
    }
    return arr;
}

static int[] bubbleSort(int[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n - 1; i++)
        for (int j = 0; j < n - i - 1; j++)
            if (arr[j] > arr[j + 1])
            {
                int temp = arr[j];
                arr[j] = arr[j + 1];
                arr[j + 1] = temp;
            }
    return arr;
}

static int[] insertionSort(int[] arr)
{
    int n = arr.Length;
    for (int i = 1; i < n; ++i)
    {
        int key = arr[i];
        int j = i - 1;
        while (j >= 0 && arr[j] > key)
        {
            arr[j + 1] = arr[j];
            j = j - 1;
        }
        arr[j + 1] = key;
    }
    return arr;
}

void sort (SortAlgorithmType algorithmType, int [] arr, AscOrderBy order = AscOrderBy.Asc)
{
 string sortType = "";
    switch (algorithmType)
    {
        case SortAlgorithmType.selectionSort:
            selectionSort(arr);
            sortType = "Selection Sort";
            break;
        case SortAlgorithmType.bubbleSort:
            bubbleSort(arr);
            sortType = "Bubble Sort";
            break;
        case SortAlgorithmType.insertSort:
            insertionSort(arr);
            sortType = "Insertion Sort";
            break;
    }

Console.Write(sortType + ": ");

    switch (order)
    {
        case AscOrderBy.Asc:
            Console.Write("Order Asc: ");
            printArray(arr);
            break;
        case AscOrderBy.Desc:
            Console.Write("Order Desc: ");
            printArrayDesc(arr);
            break;
    }
}

void sortAndRevert(SortAlgorithmType algorithmType, int[] arr, AscOrderBy order)
{
    string sortType = "";
    switch (algorithmType)
    {
        case SortAlgorithmType.selectionSort:
            selectionSort(arr);
            sortType = "Selection Sort";
            break;
        case SortAlgorithmType.bubbleSort:
            bubbleSort(arr);
            sortType = "Bubble Sort";
            break;
        case SortAlgorithmType.insertSort:
            insertionSort(arr);
            sortType = "Insertion Sort";
            break;
    }

    Console.Write(sortType + ": ");

    switch (order)
    {
        case AscOrderBy.Asc:
            Console.Write("Order Asc: ");
            printArray(arr);
            break;
        case AscOrderBy.Desc:
            arr = revertArray(arr);
            Console.Write("Order Desc: ");
            printArray(arr);
            break;
    }
}

static void printArray(int[] arr)
{
    int n = arr.Length;
    for (int i = 0; i < n; ++i)
        Console.Write(arr[i] + " ");
    Console.WriteLine();
}

static void printArrayDesc(int[] arr)
{
    int n = arr.Length -1;
    for (int i = n; i >= 0; --i)
        Console.Write(arr[i] + " ");
    Console.WriteLine();
}

static int[] revertArray(int[] arr)
{
    int n = arr.Length;
    int temp;
    for(int i = 0; i < n/2; i++)
    {
        temp = arr[n-i-1];
        arr[n - i - 1] = arr[i];
        arr[i] = temp;
    }
    return arr;
}

enum SortAlgorithmType
{
    selectionSort,
    bubbleSort,
    insertSort
}

enum AscOrderBy
{
    Asc,
    Desc
}
