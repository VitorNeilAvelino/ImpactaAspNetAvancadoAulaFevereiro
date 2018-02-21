$.validator.unobtrusive.adapters.addSingleVal("regraidademinima", "valoridademinima");

$.validator.addMethod("regraidademinima", function (value, element, valoridademinima) {
    return value &&
        Date.parseExact(value, "dd/MM/yyyy") &&
        Date.parseExact(value, "dd/MM/yyyy").addYears(valoridademinima) <= Date.today();
});