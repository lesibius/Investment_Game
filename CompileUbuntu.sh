echo "Compiling command line application"
dmcs ApplicationCMD.cs PRESENTATION/INTERFACE/*.cs PRESENTATION/LOGIC/*.cs PRESENTATION/UI/CMD/*.cs BUSINESS/COMPONENTS/DATAMANAGEMENT/*.cs INTERFACE/PRESENTATION_BUSINESS/*.cs INTERFACE/BUSINESS_DATA/*.cs DATA/ACCESSCOMPONENTS/*.cs -r:/usr/lib/cli/MySql.Data-6.4/MySql.Data.dll -r:System.Data.dll


echo "Compilation: done"