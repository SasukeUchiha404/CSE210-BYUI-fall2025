using System;

// Job class
public class Job
{
    public string _company;
    public string _jobTitle;
    public int _startYear;
    public int _endYear;

    // Meathon to display job info in the required format
    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }
}

// Resume class
public class Resume
{
    public string _name;
    public List<Job> _jobs = new List<Job>();

    // Method to display resume info
    public void Display()
    {
        Console.WriteLine($"Name: {_name}");
        Console.WriteLine("Jobs:");
        foreach (Job job in _jobs)
        {
            job.Display();
        }
    }
}

// Main program
class Program
{
    static void Main(string[] args)
    {
        // Create job instances and set properties
        Job job1 = new Job();
        job1._company = "Microsoft";
        job1._jobTitle = "Software Engineer";
        job1._startYear = 2019;
        job1._endYear = 2022;

        Job job2 = new Job();
        job2._company = "Apple";
        job2._jobTitle = "Manager";
        job2._startYear = 2022;
        job2._endYear = 2023;

        // Create resume instance and set name
        Resume myResume = new Resume();
        myResume._name = "Allison Rose";

        // Add jobs to resume
        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        // Display the complete resume
        myResume.Display();
    }
}