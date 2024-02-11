TASK_PATH=$1
TASK_NAME=$2
TASK_TYPE=$3

if [[ -z $TASK_TYPE ]] ; then 
    TASK_TYPE=classlib
fi

mkdir $TASK_PATH
pushd $TASK_PATH

mkdir $TASK_NAME
pushd $TASK_NAME
dotnet new $TASK_TYPE -lang f#
popd

mkdir $TASK_NAME.tests
pushd $TASK_NAME.tests
dotnet new nunit -lang f#
dotnet add $TASK_NAME.tests.fsproj reference ../$TASK_NAME/$TASK_NAME.fsproj
popd

dotnet new sln
dotnet sln *.sln add $TASK_NAME/$TASK_NAME.fsproj
dotnet sln *.sln add $TASK_NAME.tests/$TASK_NAME.tests.fsproj
popd
