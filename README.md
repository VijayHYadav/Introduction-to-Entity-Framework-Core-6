```
dotnet run

dotnet watch

dotnet watch --no-hot-reload

Agenda
- Queries
- Filters
- Related data
- Automapper

C# How to find closest number in List<T>

List<int> list = new List<int> { 4, 2, 10, 7 };
int number = 5;
// find closest to number
int closest = list.OrderBy(item => Math.Abs(number - item)).First();

```
![Alt text](resources/image.png)
![Alt text](resources/globleNoTracking.png)
