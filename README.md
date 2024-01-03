# Movie Management System

This project is a Movie Management System developed as a Windows Form application using C#. The system allows users to perform CRUD (Create, Read, Update, Delete) operations on movie records.

## Project Structure

The project is organized into the following directories and files:

- **MovieMgt.sln:** Visual Studio solution file.

- **Movie Management Windows Form/MovieMgt:** The main project directory.

  - **MovieMgt.csproj:** Project file for the Movie Management system.
  
  - **Program.cs:** Entry point of the application.
  
  - **appsettings.Development.json:** Configuration file for development settings.
  
  - **meta-data.json:** Metadata file.
  
  - **Pages:** Directory containing different pages of the application.
  
  - **Startup.cs:** Configuration file for application startup.
  
  - **bin:** Directory for binary files.
  
  - **wwwroot:** Directory containing web-related files like CSS, JS, and libraries.

- **Movie Management Windows Form/MovieMgt/Pages:** Directory containing pages of the application.

  - **Dashboard.cshtml:** Dashboard page.
  
  - **Forms:** Directory containing form-related pages.
  
  - **Shared:** Shared components and layouts.

- **Movie Management Windows Form/MovieMgt/Pages/Forms:** Directory containing forms for adding, editing, signing in, and signing up.

- **Movie Management Windows Form/MovieMgt/Pages/Shared:** Directory containing shared components and layouts.

- **Movie Management Windows Form/MovieMgt/Properties:** Directory containing project properties.

  - **launchSettings.json:** Configuration file for launch settings.

- **Movie Management Windows Form/MovieMgt/bin/Debug:** Directory for debug binaries.

  - **netcoreapp3.1:** Target framework version.

    - **MovieMgt.Views.dll:** DLL for Views.
    
    - **MovieMgt.exe:** Executable file for the application.
    
    - **Newtonsoft.Json.dll:** DLL for JSON handling.
    
    - **meta-data.json:** Metadata file.
    
    - **runtimes:** Directory for runtime-related files.

- **Movie Management Windows Form/MovieMgt/obj:** Directory for object files.

  - **Debug:** Debug configuration.

    - **netcoreapp3.1:** Target framework version.

      - **MovieMgt.dll:** Compiled DLL.
      
      - **MovieMgt.pdb:** Program Database file.
      
      - **_ViewImports.cshtml.g.cs:** Generated C# code for view imports.
      
      - ...

## Getting Started

To get started with this project, follow these steps:

1. Clone the repository to your local machine:

   ```bash
   git clone https://github.com/your-username/movie-management-system.git
   cd movie-management-system
   ```

2. Open the `MovieMgt.sln` solution file in Visual Studio.

3. Build and run the application.

4. Explore the different pages and forms for movie management.

## Dependencies

The project uses the following dependencies:

- **Bootstrap:** Front-end framework for styling.
  
- **jQuery:** JavaScript library for DOM manipulation.
  
- **jQuery Validation:** jQuery plugin for form validation.
  
- **jQuery Validation Unobtrusive:** jQuery plugin for client-side unobtrusive validation.

## License

This Movie Management System project is licensed under the [MIT License](LICENSE).

---
