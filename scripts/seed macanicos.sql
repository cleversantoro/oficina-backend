-- Seeds basicas para mecanicos e especialidades

INSERT INTO cad_mecanicos_especialidades (Codigo, Nome, Descricao, Ativo)
VALUES
    ('MOTOR', 'Especialista em Motor', 'Manutencao pesada de motores combustion', 1),
    ('ELETR', 'Eletrica Automotiva', 'Diagnostico e reparo de sistemas eletricos e eletronicos', 1),
    ('SUSPE', 'Suspensao e Freios', 'Ajustes de suspensao, amortecedores e freios', 1),
    ('TRANS', 'Transmissao', 'Manutencao de caixas de cambio e transmissao automatica', 1)
ON DUPLICATE KEY UPDATE
    Nome = VALUES(Nome),
    Descricao = VALUES(Descricao),
    Ativo = VALUES(Ativo),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_mecanicos (Codigo, Nome, Sobrenome, Nome_Social, Documento_Principal, Tipo_Documento,
    Data_Nascimento, Data_Admissao, Data_Demissao, Status, Especialidade_Principal_Id, Nivel, Valor_Hora,
    Carga_Horaria_Semanal, Observacoes)
VALUES
    (
        'MEC0001',
        'Carlos',
        'Silva',
        NULL,
        '12345678900',
        1,
        '1985-02-10',
        '2020-01-15',
        NULL,
        'Ativo',
        (SELECT Id FROM cad_mecanicos_especialidades WHERE Codigo = 'MOTOR'),
        'Senior',
        110.00,
        44,
        'Responsavel pela bancada de motores e diagnosticos complexos.'
    ),
    (
        'MEC0002',
        'Fernanda',
        'Costa',
        NULL,
        '98765432100',
        1,
        '1990-08-22',
        '2021-05-10',
        NULL,
        'Ativo',
        (SELECT Id FROM cad_mecanicos_especialidades WHERE Codigo = 'ELETR'),
        'Pleno',
        95.00,
        40,
        'Foco em diagnostico eletrico e instalacao de acessorios.'
    ),
    (
        'MEC0003',
        'Joao',
        'Pereira',
        NULL,
        '44556677889',
        1,
        '1988-11-12',
        '2019-03-01',
        NULL,
        'Ativo',
        (SELECT Id FROM cad_mecanicos_especialidades WHERE Codigo = 'SUSPE'),
        'Senior',
        105.00,
        44,
        'Especialista em suspensao, freios e alinhamento.'
    )
ON DUPLICATE KEY UPDATE
    Nome = VALUES(Nome),
    Sobrenome = VALUES(Sobrenome),
    Nome_Social = VALUES(Nome_Social),
    Status = VALUES(Status),
    Especialidade_Principal_Id = VALUES(Especialidade_Principal_Id),
    Nivel = VALUES(Nivel),
    Valor_Hora = VALUES(Valor_Hora),
    Carga_Horaria_Semanal = VALUES(Carga_Horaria_Semanal),
    Observacoes = VALUES(Observacoes),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_mecanicos_especialidades_rel (Mecanico_Id, Especialidade_Id, Nivel, Principal, Anotacoes)
VALUES
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0001'),
        (SELECT Id FROM cad_mecanicos_especialidades WHERE Codigo = 'MOTOR'),
        'Senior',
        1,
        'Especialidade principal'
    ),
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0002'),
        (SELECT Id FROM cad_mecanicos_especialidades WHERE Codigo = 'ELETR'),
        'Pleno',
        1,
        'Especialidade principal'
    ),
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0003'),
        (SELECT Id FROM cad_mecanicos_especialidades WHERE Codigo = 'SUSPE'),
        'Senior',
        1,
        'Especialidade principal'
    ),
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0001'),
        (SELECT Id FROM cad_mecanicos_especialidades WHERE Codigo = 'TRANS'),
        'Pleno',
        0,
        'Suporte secundario em transmissao'
    )
ON DUPLICATE KEY UPDATE
    Nivel = VALUES(Nivel),
    Principal = VALUES(Principal),
    Anotacoes = VALUES(Anotacoes),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_mecanicos_contatos (Mecanico_Id, Tipo, Valor, Principal, Observacao)
VALUES
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0001'),
        'Celular',
        '(11) 98888-0001',
        1,
        'Disponivel via WhatsApp'
    ),
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0002'),
        'Email',
        'fernanda.costa@oficina.com',
        1,
        NULL
    )
ON DUPLICATE KEY UPDATE
    Valor = VALUES(Valor),
    Principal = VALUES(Principal),
    Observacao = VALUES(Observacao),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_mecanicos_enderecos (Mecanico_Id, Tipo, Cep, Logradouro, Numero, Bairro, Cidade, Estado, Pais, Complemento, Principal)
VALUES
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0001'),
        'Residencial',
        '01001000',
        'Rua Alfa',
        '120',
        'Centro',
        'Sao Paulo',
        'SP',
        'Brasil',
        'Apto 32',
        1
    )
ON DUPLICATE KEY UPDATE
    Logradouro = VALUES(Logradouro),
    Numero = VALUES(Numero),
    Bairro = VALUES(Bairro),
    Cidade = VALUES(Cidade),
    Estado = VALUES(Estado),
    Pais = VALUES(Pais),
    Complemento = VALUES(Complemento),
    Principal = VALUES(Principal),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_mecanicos_certificacoes (Mecanico_Id, Especialidade_Id, Titulo, Instituicao, Data_Conclusao, Data_Validade, Codigo_Certificacao)
VALUES
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0002'),
        (SELECT Id FROM cad_mecanicos_especialidades WHERE Codigo = 'ELETR'),
        'Certificacao Avancada em Diagnostico Eletrico',
        'Bosch',
        '2022-07-15',
        '2025-07-14',
        'BOSCH-EL-2022'
    )
ON DUPLICATE KEY UPDATE
    Instituicao = VALUES(Instituicao),
    Data_Conclusao = VALUES(Data_Conclusao),
    Data_Validade = VALUES(Data_Validade),
    Codigo_Certificacao = VALUES(Codigo_Certificacao),
    Especialidade_Id = VALUES(Especialidade_Id),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_mecanicos_disponibilidades (Mecanico_Id, Dia_Semana, Hora_Inicio, Hora_Fim, Capacidade_Atendimentos)
VALUES
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0001'),
        1,
        '08:00:00',
        '17:00:00',
        4
    ),
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0002'),
        3,
        '10:00:00',
        '19:00:00',
        5
    )
ON DUPLICATE KEY UPDATE
    Hora_Inicio = VALUES(Hora_Inicio),
    Hora_Fim = VALUES(Hora_Fim),
    Capacidade_Atendimentos = VALUES(Capacidade_Atendimentos),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_mecanicos_experiencias (Mecanico_Id, Empresa, Cargo, Data_Inicio, Data_Fim, Resumo_Atividades)
VALUES
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0001'),
        'Auto Center Prime',
        'Mecanico Senior',
        '2015-01-01',
        '2019-12-15',
        'Liderou equipe de manutencao pesada e treinou mecanicos junior.'
    ),
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0002'),
        'Eletrica AutoTech',
        'Especialista Eletrica',
        '2017-03-01',
        '2021-04-30',
        'Responsavel por diagnostico de sistemas eletricos e implementacao de novas ferramentas.'
    )
ON DUPLICATE KEY UPDATE
    Empresa = VALUES(Empresa),
    Cargo = VALUES(Cargo),
    Data_Inicio = VALUES(Data_Inicio),
    Data_Fim = VALUES(Data_Fim),
    Resumo_Atividades = VALUES(Resumo_Atividades),
    Updated_At = CURRENT_TIMESTAMP(6);

INSERT INTO cad_mecanicos_documentos (Mecanico_Id, Tipo, Numero, Data_Emissao, Data_Validade, Orgao_Expedidor, Arquivo_Url)
VALUES
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0001'),
        'CRMV',
        'CRMV-SP-5566',
        '2019-01-10',
        '2024-01-09',
        'CRMV-SP',
        NULL
    ),
    (
        (SELECT Id FROM cad_mecanicos WHERE Codigo = 'MEC0002'),
        'CREA',
        'CREA-SP-7788',
        '2020-03-05',
        '2025-03-04',
        'CREA-SP',
        NULL
    )
ON DUPLICATE KEY UPDATE
    Data_Emissao = VALUES(Data_Emissao),
    Data_Validade = VALUES(Data_Validade),
    Orgao_Expedidor = VALUES(Orgao_Expedidor),
    Arquivo_Url = VALUES(Arquivo_Url),
    Updated_At = CURRENT_TIMESTAMP(6);
