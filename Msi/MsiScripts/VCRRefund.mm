;  Project file for VCRRefund
#define PRODUCT_CONTACT_NAME            Henry Eng
#include "Image.MMH"
#define? VCRREFUND_FILES              ..\appfiles\*.*
#define APP_DATA_DIR D:\Airgroup\Data\Image\VCRRefund


<$DirectoryTree Key="APPDATADIR" Dir="<$APP_DATA_DIR>"  MAKE="Y">


<$DirectoryTree Key="INSTALLDIR" Dir="<$APP_INSTALL_DIR>" CHANGE="\" \
      PrimaryFolder="Y">

<$Files "<$VCRREFUND_FILES>" DestDir="INSTALLDIR">

