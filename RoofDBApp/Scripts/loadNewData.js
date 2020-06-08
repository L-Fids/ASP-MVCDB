function processData(data) {
    // select the parent and empty it
    var target = document.getElementById("grid-container");
    target.innerHTML = "";

    for (var i = 0; i < data.length; i++) {
        // place data at position i in customer variable
        var customer = data[i];

        // create grid-inner div
        var gridInnerClassName = "grid-inner";
        var createDivGridInner = document.createElement("div");
        createDivGridInner.setAttribute("class", gridInnerClassName);

        // create grid-content div
        var gridContentClassName = "grid-content";
        var gridContentIdName = "grid-define" + "-" + i;
        var createDivGridContent = document.createElement("div");
        createDivGridContent.setAttribute("class", gridContentClassName);
        createDivGridContent.setAttribute("id", gridContentIdName);

        // create customer data divs
        var customerDataName = "customer-name";
        var createDivDataName = document.createElement("div");
        createDivDataName.innerHTML = customer.FirstName + " " + customer.LastName;
        createDivDataName.setAttribute("class", customerDataName);

        var customerAddressName = "customer-address";
        var createDivDataAddress = document.createElement("div");
        createDivDataAddress.innerHTML = customer.Address;
        createDivDataAddress.setAttribute("class", customerAddressName);

        var customerCityName = "customer-city";
        var createDivDataCity = document.createElement("div");
        createDivDataCity.innerHTML = customer.City;
        createDivDataCity.setAttribute("class", customerCityName);

        var customerProvinceName = "customer-province";
        var createDivDataProvince = document.createElement("div");
        createDivDataProvince.innerHTML = customer.Province;
        createDivDataProvince.setAttribute("class", customerProvinceName);

        var customerPostalCodeName = "customer-postalCode";
        var createDivDataPostalCode = document.createElement("div");
        createDivDataPostalCode.innerHTML = customer.PostalCode;
        createDivDataPostalCode.setAttribute("class", customerPostalCodeName);

        var customerPhoneNumberName = "customer-phonenumber";
        var createDivDataPhoneNumber = document.createElement("div");
        createDivDataPhoneNumber.innerHTML = customer.PhoneNumber;
        createDivDataPhoneNumber.setAttribute("class", customerPhoneNumberName);

        var customerEmailName = "customer-email";
        var createDivDataEmail = document.createElement("div");
        createDivDataEmail.innerHTML = customer.Email;
        createDivDataEmail.setAttribute("class", customerEmailName);

        var customerLeadSourceName = "customer-leadsource";
        var createDivDataLeadSource = document.createElement("div");
        createDivDataLeadSource.innerHTML = customer.LeadSource;
        createDivDataLeadSource.setAttribute("class", customerLeadSourceName);

        var customerStatusName = "customer-status";
        var createDivDataStatus = document.createElement("div");
        createDivDataStatus.innerHTML = customer.Status;
        createDivDataStatus.setAttribute("class", customerStatusName);

        var customerNotesName = "customer-notes";
        var createDivDataNotes = document.createElement("div");
        createDivDataNotes.innerHTML = customer.Notes;
        createDivDataNotes.setAttribute("class", customerNotesName);

        // add data divs to inner grid
        createDivGridInner.appendChild(createDivDataName);
        createDivGridInner.appendChild(createDivDataAddress);
        createDivGridInner.appendChild(createDivDataCity);
        createDivGridInner.appendChild(createDivDataProvince);
        createDivGridInner.appendChild(createDivDataPostalCode);
        createDivGridInner.appendChild(createDivDataPhoneNumber);
        createDivGridInner.appendChild(createDivDataEmail);
        createDivGridInner.appendChild(createDivDataLeadSource);
        createDivGridInner.appendChild(createDivDataStatus);
        createDivGridInner.appendChild(createDivDataNotes);

        // create crud-options div
        var customerID = customer.CustomerID;
        var crudDiv = document.createElement("div");
        var crudDivName = "crud-options";
        crudDiv.setAttribute("class", crudDivName);

        // create details link
        var aDetails = document.createElement('a');
        var infoButtonClass = "btn btn-info btn-xs";
        aDetails.setAttribute("class", infoButtonClass);

        var hrefClass = `/Customer/Details/${customerID}`
        aDetails.setAttribute("href", hrefClass);
        aDetails.innerHTML = "Details";

        // create Edit link
        var aEdit = document.createElement('a');
        var primaryButtonClass = "btn btn-primary btn-xs";
        aEdit.setAttribute("class", primaryButtonClass);

        var hrefClass = `/Customer/Edit/${customerID}`
        aEdit.setAttribute("href", hrefClass);
        aEdit.innerHTML = "Edit";

        // create Delete link 
        var aDelete = document.createElement('a');
        var dangerButtonClass = "btn btn-danger btn-xs";
        aDelete.setAttribute("class", dangerButtonClass);

        var hrefClass = `/Customer/Delete/${customerID}`
        aDelete.setAttribute("href", hrefClass);
        aDelete.innerHTML = "Delete";

        // add links to crud options div
        crudDiv.appendChild(aDetails);
        crudDiv.appendChild(aEdit);
        crudDiv.appendChild(aDelete);
        createDivGridInner.append(crudDiv);

        //add inner grid to grid content
        createDivGridContent.appendChild(createDivGridInner);

        //add grid content to grid container
        target.append(createDivGridContent);
    }
}
