section_line=$(echo "#########################################################")
justify=$(echo "               ")

echo $section_line
echo "$justify Retrieving files to compile"
echo $section_line
echo "Retrieving dll files"
dll_files=$(echo "-r:/usr/lib/cli/MySql.Data-6.4/MySql.Data.dll -r:System.Data.dll")
echo "Retrieving UI files"
UI_Global=$(echo "PRESENTATION/INTERFACE/*.cs PRESENTATION/LOGIC/*.cs")
UI_CMD=$(echo "PRESENTATION/UI/CMD/*.cs")
echo "Retrieving business files"
BUSINESS=$(echo "BUSINESS/COMPONENTS/DATAMANAGEMENT/*.cs BUSINESS/WORKFLOW/*.cs")
echo "Retrieving data files"
DATA=$(echo "DATA/ACCESSCOMPONENTS/*.cs")
echo "Retrieving interface files"
INTERFACE=$(echo "INTERFACE/PRESENTATION_BUSINESS/*.cs INTERFACE/BUSINESS_DATA/*.cs")

echo

echo $section_line
echo "$justify Compiling Applications"
echo $section_line
echo "Compiling command line application"
dmcs ApplicationCMD.cs $UI_Global $UI_CMD $BUSINESS $DATA $INTERFACE $dll_files
echo

echo "Compilation: done"

Presentation=$(ls PRESENTATION)


echo $section_line
