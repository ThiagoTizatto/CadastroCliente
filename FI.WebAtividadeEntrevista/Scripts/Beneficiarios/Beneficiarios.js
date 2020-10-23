var beneficiarios = [];


function listarBeneficiarios() {

    for (var idx in beneficiarios) {
        let HTML = getTRBeneficiarioHTML(beneficiarios[idx]);

        $("#modal-beneficiario table tbody").append(HTML);
    }

}
function criaBeneficiario(CPF, nome) {
    return {
        CPF: CPF.trim(),
        Nome: nome.trim()
    }
}

function getTRBeneficiarioHTML(beneficiario) {
    let HTML = `
                  <tr data-cpf="${beneficiario.CPF}">
                  <th scope="row">${beneficiario.CPF}</th>
                  <td>${beneficiario.Nome}</td>
                  <td>
                    <button type="button" name="alterar" class="btn btn-primary" onclick="editarBeneficiario('${beneficiario.CPF}')">Alterar</button>
                 </td>
                  <td>
                    <button type="button" name"excluir" class="btn btn-primary" onclick="excluirBeneficiario('${beneficiario.CPF}')">Excluir</button>
                  </td>
                  </tr>`;

    return HTML;
}

function editarBeneficiario(CPF) {

    let beneficiario = obterBeneficiarioPorCPF(CPF);

    $("#NomeBeneficiario").val(beneficiario.Nome);
    $("#CPFBeneficiario").val(beneficiario.CPF);

}

function obterBeneficiarioPorCPF(CPF) {

    return beneficiarios[beneficiarios.map(x => x.CPF).indexOf(CPF)];
}

function excluirBeneficiario(CPF) {

    beneficiarios.splice(beneficiarios.map(x => x.CPF).indexOf(CPF), 1);
    $(`#modal-beneficiario table tr[data-cpf="${CPF}"]`).remove();

}

function atualizaBeneficiario(beneficiario, nome) {
    beneficiario.Nome = nome;

    $(`#modal-beneficiario table tr[data-cpf="${beneficiario.CPF}"]`).find("td:eq(0)").html(beneficiario.Nome);

}

function incluirBeneficiario() {
    let $nome = $("#NomeBeneficiario"),
        $CPF = $("#CPFBeneficiario");

    let beneficiario = criaBeneficiario($CPF.val(), $nome.val())

    if (!validaBeneficiario(beneficiario))
        return;

    let beneficiarioTabela = obterBeneficiarioPorCPF(beneficiario.CPF);
    if (beneficiarioTabela) {
        atualizaBeneficiario(beneficiarioTabela, beneficiario.Nome);
    } else {

        beneficiarios.push(beneficiario);

        let HTML = getTRBeneficiarioHTML(beneficiario);

        $("#modal-beneficiario table tbody").append(HTML);
    }

   
    $nome.val("");
    $CPF.val("");

}

function validaBeneficiario(beneficiario) {
    if (beneficiario.Nome == "" || beneficiario.CPF == "")
        return false;

    if (!validarCPF(beneficiario.CPF)) {
        alert("CPF inválido");
        return false;
    }


    return true;
}


function validarCPF(CPF) {
    CPF = CPF.replace(/[^\d]+/g, '');
    if (CPF == '') return false;
    // Elimina CPFs invalidos conhecidos	
    if (CPF.length != 11 ||
        CPF == "00000000000" ||
        CPF == "11111111111" ||
        CPF == "22222222222" ||
        CPF == "33333333333" ||
        CPF == "44444444444" ||
        CPF == "55555555555" ||
        CPF == "66666666666" ||
        CPF == "77777777777" ||
        CPF == "88888888888" ||
        CPF == "99999999999")
        return false;
    // Valida 1o digito	
    add = 0;
    for (i = 0; i < 9; i++)
        add += parseInt(CPF.charAt(i)) * (10 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(CPF.charAt(9)))
        return false;
    // Valida 2o digito	
    add = 0;
    for (i = 0; i < 10; i++)
        add += parseInt(CPF.charAt(i)) * (11 - i);
    rev = 11 - (add % 11);
    if (rev == 10 || rev == 11)
        rev = 0;
    if (rev != parseInt(CPF.charAt(10)))
        return false;
    return true;
}