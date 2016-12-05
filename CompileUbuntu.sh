section_line=$(echo "#########################################################")
justify=$(echo "               ")

#################################################################################################
#				Retrieving Files to Compile					#
#################################################################################################

echo $section_line
echo "$justify Retrieving files to compile"
echo $section_line

#					DLL Files						#

echo "Retrieving dll files"
dll_files=$(echo "-r:/usr/lib/cli/MySql.Data-6.4/MySql.Data.dll -r:System.Data.dll")

#					UI Files						#

echo "Retrieving UI files"
UI_Global=$(echo "PRESENTATION/INTERFACE/*.cs PRESENTATION/LOGIC/*.cs")
UI_CMD=$(echo "PRESENTATION/UI/CMD/*.cs")

#					BUSINESS Files						#
echo "Retrieving business files"
BUSINESS=$(echo "BUSINESS/COMPONENTS/DATAMANAGEMENT/*.cs BUSINESS/WORKFLOW/*.cs")

echo "Retrieving FinanceLib"
FINANCELIB=$(echo "BUSINESS/COMPONENTS/FINANCE/FinanceLib/ValueOperator/*.cs BUSINESS/COMPONENTS/FINANCE/FinanceLib/Investment/*.cs BUSINESS/COMPONENTS/FINANCE/FinanceLib/Measurement/*.cs")

echo "Retrieving data files"
DATA=$(echo "DATA/ACCESSCOMPONENTS/*.cs")
echo "Retrieving interface files"
INTERFACE=$(echo "INTERFACE/PRESENTATION_BUSINESS/*.cs INTERFACE/BUSINESS_DATA/*.cs")

echo

echo $section_line
echo "$justify Compiling Applications"
echo $section_line
echo "Compiling command line application"
echo "Aborted"
#dmcs ApplicationCMD.cs $UI_Global $UI_CMD $BUSINESS $FINANCELIB $DATA $INTERFACE $dll_files
echo  "Compiling test application"
dmcs BUSINESS/COMPONENTS/FINANCE/FinanceLib/Test/*.cs $UI_Global $UI_CMD $BUSINESS $FINANCELIB $DATA $INTERFACE $dll_files

echo "Compilation: done"

Presentation=$(ls PRESENTATION)


echo $section_line
