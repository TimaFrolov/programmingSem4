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

mkdir $TASK_NAME.Tests
pushd $TASK_NAME.Tests
dotnet new nunit -lang f#
dotnet add $TASK_NAME.Tests.fsproj package FsUnit -v 5.0.0
dotnet add $TASK_NAME.Tests.fsproj reference ../$TASK_NAME/$TASK_NAME.fsproj
popd

dotnet new sln
dotnet sln *.sln add $TASK_NAME/$TASK_NAME.fsproj
dotnet sln *.sln add $TASK_NAME.Tests/$TASK_NAME.Tests.fsproj
popd
