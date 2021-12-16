# MMSSTV.Net

What is this application?
This is a demonstration application to show how to use the MMSSTV engine in a C# application. It is not intended to be a production quality application as it includes minimal error checking and almost no exception handling. I may add more error handling as time permits. I developed this application as an exercise in interfacing with the MMSSTV engine and to serve as an example to help others who wish to use the MMSSTV engine in their C# applications.

What is the MMSSTV engine?
The MMSSTV engine is a 32 bit DLL developed by Makoto Mori that permits application developers to integrate SSTV into their application. This program doesn't include a copy of the MMSSTV engine DLL file. The DLL and the documentation associated with it are available at https://hamsoft.ca/pages/programmers/mmsstv-engine.php. The documentation includes sample programs written using C++ and VB6. This application duplicates the functionality of those sample applications using C# and the .Net Framework.

How can I use the MMSSTV engine with my C# application?
To use the engine in your C# application you will first need to download the MMSSTV engine DLL using the link above. Place this DLL in the same folder as your application's executable file (.exe). Then include the SSTVENGWrapper file in your project. After that you should be able to call the functions in the DLL. The documentation along with this and the other sample applications should get you running from there.

Outstanding Issues:
- Applications need to use the options dialog provided in the DLL, which doesn't show names for the audio device. only ordinal numbers. Also the dialog doesn't permit selecting separate input and output audio devices. Need to find a way to set both the input and output audio devices separately. I have included classes in the project for enumerating the input and output audio devices. This was so I could associate names with the ordinal numbers sued by the MMSSTV engine options dialog.
