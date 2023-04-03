public class TaskController : Controller 
{ 
    private readonly AppDbContext _dbContext; 

    public TaskController(AppDbContext dbContext) 
    { 
        _dbContext = dbContext; 
    } 

    public IActionResult Index(string searchString, bool? isCompleted) 
    { 
        var tasks = _dbContext.Tasks.ToList(); 

        if (!string.IsNullOrEmpty(searchString)) 
        { 
            tasks = tasks.Where(t => t.Title.Contains(searchString)).ToList(); 
        } 

        if (isCompleted.HasValue) 
        { 
            tasks = tasks.Where(t => t.IsCompleted == isCompleted.Value).ToList(); 
        } 

        return View(tasks); 
    } 

    public IActionResult Create() 
    { 
        return View(); 
    } 

    [HttpPost] 
    public IActionResult Create(Task task) 
    { 
        _dbContext.Tasks.Add(task); 
        _dbContext.SaveChanges(); 

        return RedirectToAction("Index"); 
    } 

    public IActionResult Edit(int id) 
    { 
        var task = _dbContext.Tasks.FirstOrDefault(t => t.Id == id); 

        if (task == null) 
        { 
            return NotFound(); 
        } 

        return View(task); 
    } 

    [HttpPost] 
    public IActionResult Edit(Task task) 
    { 
        _dbContext.Tasks.Update(task); 
        _dbContext.SaveChanges(); 

        return RedirectToAction("Index"); 
    } 

    public IActionResult Delete(int id)
