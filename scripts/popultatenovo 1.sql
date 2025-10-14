USE oficina_db;

SET FOREIGN_KEY_CHECKS = 0;
TRUNCATE TABLE fin_nfes;
TRUNCATE TABLE fin_pagamentos;
TRUNCATE TABLE os_itens;
TRUNCATE TABLE os_ordens;
TRUNCATE TABLE est_movimentacoes;
TRUNCATE TABLE est_pecas;
TRUNCATE TABLE cad_veiculos;
TRUNCATE TABLE cad_veiculos_modelos;
TRUNCATE TABLE cad_veiculos_marcas;
TRUNCATE TABLE cad_clientes_anexos;
TRUNCATE TABLE cad_clientes_lgpd_consentimentos;
TRUNCATE TABLE cad_clientes_financeiro;
TRUNCATE TABLE cad_clientes_indicacoes;
TRUNCATE TABLE cad_clientes_contatos;
TRUNCATE TABLE cad_clientes_enderecos;
TRUNCATE TABLE cad_clientes_pf;
TRUNCATE TABLE cad_clientes_pj;
TRUNCATE TABLE cad_fornecedores;
TRUNCATE TABLE cad_mecanicos;
TRUNCATE TABLE cad_clientes;
TRUNCATE TABLE cad_clientes_origens;
SET FOREIGN_KEY_CHECKS = 1;

INSERT INTO cad_clientes_origens (Nome, Descricao, Created_At, Updated_At)
VALUES ('Campanha Digital', 'Leads de anúncios on-line', NOW(), NULL);
SET @orig1 = LAST_INSERT_ID();

INSERT INTO cad_clientes_origens (Nome, Descricao, Created_At, Updated_At)
VALUES ('Indicação', 'Clientes indicados por parceiros', NOW(), NULL);
SET @orig2 = LAST_INSERT_ID();

INSERT INTO cad_clientes_origens (Nome, Descricao, Created_At, Updated_At)
VALUES ('Walk-in', 'Atendimento presencial', NOW(), NULL);
SET @orig3 = LAST_INSERT_ID();

INSERT INTO cad_clientes_origens (Nome, Descricao, Created_At, Updated_At)
VALUES ('Telefone', 'Contato via call center', NOW(), NULL);
SET @orig4 = LAST_INSERT_ID();

INSERT INTO cad_clientes_origens (Nome, Descricao, Created_At, Updated_At)
VALUES ('Aplicativo', 'Cadastro feito no app', NOW(), NULL);
SET @orig5 = LAST_INSERT_ID();

INSERT INTO cad_clientes (Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES (20241010001, 'Mariana Souza', 'Mariana Souza', '12345678901', 1, 1, 0, 'Nova cliente via campanha digital', 1, '(11) 91234-1001', 'mariana.souza@example.com', @orig1, NOW(), NULL);
SET @cli_pf1 = LAST_INSERT_ID();

INSERT INTO cad_clientes (Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES (20241010002, 'Carlos Barbosa', 'Carlos Barbosa', '98765432100', 1, 1, 1, 'Cliente VIP indicado por parceiro', 2, '(11) 98888-2002', 'carlos.barbosa@example.com', @orig2, NOW(), NULL);
SET @cli_pf2 = LAST_INSERT_ID();

INSERT INTO cad_clientes (Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES (20241010003, 'Luciana Prado', 'Luciana Prado', '45678912300', 1, 2, 0, 'Aguardando aprovação de orçamento', 3, '(11) 95555-3003', 'luciana.prado@example.com', @orig3, NOW(), NULL);
SET @cli_pf3 = LAST_INSERT_ID();

INSERT INTO cad_clientes (Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES (20241010004, 'Eduardo Martins', 'Eduardo Martins', '74185296300', 1, 1, 0, 'Prefere atendimentos noturnos', 4, '(11) 94444-4004', 'eduardo.martins@example.com', @orig4, NOW(), NULL);
SET @cli_pf4 = LAST_INSERT_ID();

INSERT INTO cad_clientes (Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES (20241010005, 'Fernanda Reis', 'Fernanda Reis', '36925814700', 1, 1, 0, 'Parceria com empresa local', 5, '(11) 93333-5005', 'fernanda.reis@example.com', @orig5, NOW(), NULL);
SET @cli_pf5 = LAST_INSERT_ID();

INSERT INTO cad_clientes (Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES (20241010006, 'Auto Peças Horizonte LTDA', 'Auto Horizonte', '11222333444455', 2, 1, 0, 'Fornecedor eventual de peças', 1, '(11) 3456-6006', 'contato@autohorizonte.com.br', @orig1, NOW(), NULL);
SET @cli_pj1 = LAST_INSERT_ID();

INSERT INTO cad_clientes (Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES (20241010007, 'TechFleet Serviços LTDA', 'TechFleet', '55443322110088', 2, 1, 1, 'Contrato de manutenção de frota', 2, '(11) 4000-7007', 'suporte@techfleet.com.br', @orig2, NOW(), NULL);
SET @cli_pj2 = LAST_INSERT_ID();

INSERT INTO cad_clientes (Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES (20241010008, 'Logparts Comércio LTDA', 'Logparts', '00334455667788', 2, 1, 0, 'Parceiro logístico', 3, '(11) 3666-8008', 'suporte@logparts.com.br', @orig3, NOW(), NULL);
SET @cli_pj3 = LAST_INSERT_ID();

INSERT INTO cad_clientes (Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES (20241010009, 'FleetPlus Transportes SA', 'FleetPlus', '00998877665544', 2, 1, 1, 'Cliente corporativo', 4, '(11) 3222-9009', 'contato@fleetplus.com.br', @orig4, NOW(), NULL);
SET @cli_pj4 = LAST_INSERT_ID();

INSERT INTO cad_clientes (Codigo, Nome, NomeExibicao, Documento, Tipo, Status, Vip, Observacoes, OrigemCadastroId, Telefone, Email, Origem_Id, Created_At, Updated_At)
VALUES (20241010010, 'Motores Brasil Indústria LTDA', 'Motores Brasil', '00778899001122', 2, 1, 0, 'Fornece componentes especiais', 5, '(11) 3111-0010', 'comercial@motoresbr.com', @orig5, NOW(), NULL);
SET @cli_pj5 = LAST_INSERT_ID();

INSERT INTO cad_clientes_pf (Cliente_Id, Cpf, Rg, Data_Nascimento, Genero, Estado_Civil, Profissao, Created_At, Updated_At)
VALUES (@cli_pf1, '12345678901', '45.123.987-1', '1990-05-12', 'Feminino', 'Solteira', 'Designer', NOW(), NULL);

INSERT INTO cad_clientes_pf (Cliente_Id, Cpf, Rg, Data_Nascimento, Genero, Estado_Civil, Profissao, Created_At, Updated_At)
VALUES (@cli_pf2, '98765432100', '12.987.654-2', '1985-03-22', 'Masculino', 'Casado', 'Analista de Sistemas', NOW(), NULL);

INSERT INTO cad_clientes_pf (Cliente_Id, Cpf, Rg, Data_Nascimento, Genero, Estado_Civil, Profissao, Created_At, Updated_At)
VALUES (@cli_pf3, '45678912300', '98.321.456-9', '1993-11-01', 'Feminino', 'Divorciada', 'Administradora', NOW(), NULL);

INSERT INTO cad_clientes_pf (Cliente_Id, Cpf, Rg, Data_Nascimento, Genero, Estado_Civil, Profissao, Created_At, Updated_At)
VALUES (@cli_pf4, '74185296300', '74.185.296-3', '1988-07-30', 'Masculino', 'Solteiro', 'Professor', NOW(), NULL);

INSERT INTO cad_clientes_pf (Cliente_Id, Cpf, Rg, Data_Nascimento, Genero, Estado_Civil, Profissao, Created_At, Updated_At)
VALUES (@cli_pf5, '36925814700', '36.925.814-7', '1995-01-18', 'Feminino', 'Casada', 'Arquiteta', NOW(), NULL);

INSERT INTO cad_clientes_pj (Cliente_Id, Cnpj, Razao_Social, Nome_Fantasia, Inscricao_Estadual, Inscricao_Municipal, Responsavel, Created_At, Updated_At)
VALUES (@cli_pj1, '11222333444455', 'Auto Peças Horizonte LTDA', 'Auto Horizonte', '112.223.334.444', '12.345.678', 'Paulo Henrique', NOW(), NULL);

INSERT INTO cad_clientes_pj (Cliente_Id, Cnpj, Razao_Social, Nome_Fantasia, Inscricao_Estadual, Inscricao_Municipal, Responsavel, Created_At, Updated_At)
VALUES (@cli_pj2, '55443322110088', 'TechFleet Serviços LTDA', 'TechFleet', '554.433.221.100', '98.765.432', 'Renata Lima', NOW(), NULL);

INSERT INTO cad_clientes_pj (Cliente_Id, Cnpj, Razao_Social, Nome_Fantasia, Inscricao_Estadual, Inscricao_Municipal, Responsavel, Created_At, Updated_At)
VALUES (@cli_pj3, '00334455667788', 'Logparts Comércio LTDA', 'Logparts', '003.344.556.677', '11.222.333', 'Fernando Costa', NOW(), NULL);

INSERT INTO cad_clientes_pj (Cliente_Id, Cnpj, Razao_Social, Nome_Fantasia, Inscricao_Estadual, Inscricao_Municipal, Responsavel, Created_At, Updated_At)
VALUES (@cli_pj4, '00998877665544', 'FleetPlus Transportes SA', 'FleetPlus', '009.988.776.655', '66.555.444', 'Marcela Queiroz', NOW(), NULL);

INSERT INTO cad_clientes_pj (Cliente_Id, Cnpj, Razao_Social, Nome_Fantasia, Inscricao_Estadual, Inscricao_Municipal, Responsavel, Created_At, Updated_At)
VALUES (@cli_pj5, '00778899001122', 'Motores Brasil Indústria LTDA', 'Motores Brasil', '007.788.990.011', '55.444.333', 'Leonardo Azevedo', NOW(), NULL);

INSERT INTO cad_clientes_enderecos (Cliente_Id, Tipo, Cep, Logradouro, Numero, Bairro, Cidade, Estado, Pais, Complemento, Principal, Created_At, Updated_At)
VALUES (@cli_pf1, 1, '01310-100', 'Av. Paulista', '1000', 'Bela Vista', 'São Paulo', 'SP', 'Brasil', 'Apto 101', 1, NOW(), NULL);

INSERT INTO cad_clientes_enderecos (Cliente_Id, Tipo, Cep, Logradouro, Numero, Bairro, Cidade, Estado, Pais, Complemento, Principal, Created_At, Updated_At)
VALUES (@cli_pf2, 1, '04001-001', 'Rua Vergueiro', '250', 'Paraíso', 'São Paulo', 'SP', 'Brasil', NULL, 1, NOW(), NULL);

INSERT INTO cad_clientes_enderecos (Cliente_Id, Tipo, Cep, Logradouro, Numero, Bairro, Cidade, Estado, Pais, Complemento, Principal, Created_At, Updated_At)
VALUES (@cli_pf3, 1, '04567-020', 'Av. Faria Lima', '1500', 'Itaim Bibi', 'São Paulo', 'SP', 'Brasil', 'Conj. 1207', 1, NOW(), NULL);

INSERT INTO cad_clientes_enderecos (Cliente_Id, Tipo, Cep, Logradouro, Numero, Bairro, Cidade, Estado, Pais, Complemento, Principal, Created_At, Updated_At)
VALUES (@cli_pf4, 2, '06000-020', 'Rua do Progresso', '900', 'Centro', 'Osasco', 'SP', 'Brasil', 'Sala 05', 1, NOW(), NULL);

INSERT INTO cad_clientes_enderecos (Cliente_Id, Tipo, Cep, Logradouro, Numero, Bairro, Cidade, Estado, Pais, Complemento, Principal, Created_At, Updated_At)
VALUES (@cli_pf5, 3, '07070-030', 'Rua das Flores', '45', 'Jardim', 'Guarulhos', 'SP', 'Brasil', NULL, 1, NOW(), NULL);

INSERT INTO cad_clientes_contatos (Cliente_Id, Tipo, Valor, Principal, Observacao, Created_At, Updated_At)
VALUES (@cli_pf1, 2, '(11) 91234-1001', 1, 'WhatsApp principal', NOW(), NULL);

INSERT INTO cad_clientes_contatos (Cliente_Id, Tipo, Valor, Principal, Observacao, Created_At, Updated_At)
VALUES (@cli_pf2, 3, 'carlos.barbosa@example.com', 1, 'E-mail preferencial', NOW(), NULL);

INSERT INTO cad_clientes_contatos (Cliente_Id, Tipo, Valor, Principal, Observacao, Created_At, Updated_At)
VALUES (@cli_pf3, 1, '(11) 95555-3003', 1, 'Telefone residencial', NOW(), NULL);

INSERT INTO cad_clientes_contatos (Cliente_Id, Tipo, Valor, Principal, Observacao, Created_At, Updated_At)
VALUES (@cli_pf4, 2, '(11) 94444-4004', 1, 'Contato urgente', NOW(), NULL);

INSERT INTO cad_clientes_contatos (Cliente_Id, Tipo, Valor, Principal, Observacao, Created_At, Updated_At)
VALUES (@cli_pf5, 3, 'fernanda.reis@example.com', 1, 'E-mail corporativo', NOW(), NULL);

INSERT INTO cad_clientes_indicacoes (Cliente_Id, Indicador_Nome, Indicador_Telefone, Observacao, Created_At, Updated_At)
VALUES (@cli_pf1, 'Juliana Ramos', '(11) 98888-0000', 'Amiga de infância', NOW(), NULL);

INSERT INTO cad_clientes_indicacoes (Cliente_Id, Indicador_Nome, Indicador_Telefone, Observacao, Created_At, Updated_At)
VALUES (@cli_pf2, 'Pedro Oliveira', '(11) 97777-3333', 'Colega de trabalho', NOW(), NULL);

INSERT INTO cad_clientes_indicacoes (Cliente_Id, Indicador_Nome, Indicador_Telefone, Observacao, Created_At, Updated_At)
VALUES (@cli_pf3, 'Fernanda Dias', '(11) 96666-2222', 'Contato via telefone', NOW(), NULL);

INSERT INTO cad_clientes_indicacoes (Cliente_Id, Indicador_Nome, Indicador_Telefone, Observacao, Created_At, Updated_At)
VALUES (@cli_pf4, 'Marcos Lima', '(11) 95555-1111', 'Vizinho', NOW(), NULL);

INSERT INTO cad_clientes_indicacoes (Cliente_Id, Indicador_Nome, Indicador_Telefone, Observacao, Created_At, Updated_At)
VALUES (@cli_pf5, 'Ana Ferreira', '(11) 93333-4444', 'Parceira de negócios', NOW(), NULL);

INSERT INTO cad_clientes_lgpd_consentimentos (Cliente_Id, Tipo, Aceito, Data, Valido_Ate, Observacoes, Canal, Created_At, Updated_At)
VALUES (@cli_pf1, 1, 1, NOW(), DATE_ADD(NOW(), INTERVAL 1 YEAR), 'Aceitou receber promoções', 'API', NOW(), NULL);

INSERT INTO cad_clientes_lgpd_consentimentos (Cliente_Id, Tipo, Aceito, Data, Valido_Ate, Observacoes, Canal, Created_At, Updated_At)
VALUES (@cli_pf2, 2, 1, NOW(), DATE_ADD(NOW(), INTERVAL 1 YEAR), 'Compartilhamento de dados', 'Portal', NOW(), NULL);

INSERT INTO cad_clientes_lgpd_consentimentos (Cliente_Id, Tipo, Aceito, Data, Valido_Ate, Observacoes, Canal, Created_At, Updated_At)
VALUES (@cli_pf3, 3, 1, NOW(), DATE_ADD(NOW(), INTERVAL 2 YEAR), 'Comunicados de serviço', 'Telefone', NOW(), NULL);

INSERT INTO cad_clientes_lgpd_consentimentos (Cliente_Id, Tipo, Aceito, Data, Valido_Ate, Observacoes, Canal, Created_At, Updated_At)
VALUES (@cli_pf4, 1, 0, NOW(), NULL, 'Não deseja marketing', 'Aplicativo', NOW(), NULL);

INSERT INTO cad_clientes_lgpd_consentimentos (Cliente_Id, Tipo, Aceito, Data, Valido_Ate, Observacoes, Canal, Created_At, Updated_At)
VALUES (@cli_pf5, 3, 1, NOW(), DATE_ADD(NOW(), INTERVAL 3 YEAR), 'Notificações sobre agenda', 'API', NOW(), NULL);

INSERT INTO cad_clientes_financeiro (Cliente_Id, Limite_Credito, Prazo_Pagamento, Bloqueado, Observacoes, Created_At, Updated_At)
VALUES (@cli_pf1, 5000.00, 15, 0, 'Limite padrão PF', NOW(), NULL);

INSERT INTO cad_clientes_financeiro (Cliente_Id, Limite_Credito, Prazo_Pagamento, Bloqueado, Observacoes, Created_At, Updated_At)
VALUES (@cli_pf2, 8000.00, 20, 0, 'Cliente VIP', NOW(), NULL);

INSERT INTO cad_clientes_financeiro (Cliente_Id, Limite_Credito, Prazo_Pagamento, Bloqueado, Observacoes, Created_At, Updated_At)
VALUES (@cli_pf3, 3000.00, 10, 0, 'Liberado após análise', NOW(), NULL);

INSERT INTO cad_clientes_financeiro (Cliente_Id, Limite_Credito, Prazo_Pagamento, Bloqueado, Observacoes, Created_At, Updated_At)
VALUES (@cli_pf4, 4500.00, 20, 0, 'Perfil regular', NOW(), NULL);

INSERT INTO cad_clientes_financeiro (Cliente_Id, Limite_Credito, Prazo_Pagamento, Bloqueado, Observacoes, Created_At, Updated_At)
VALUES (@cli_pf5, 6000.00, 25, 0, 'Cliente corporativo pequeno porte', NOW(), NULL);

INSERT INTO cad_clientes_anexos (Cliente_Id, Nome, Tipo, Url, Observacao, Created_At, Updated_At)
VALUES (@cli_pf1, 'RG_Mariana.pdf', 'application/pdf', 'https://storage.oficina.com/docs/RG_Mariana.pdf', 'Doc de identificação', NOW(), NULL);

INSERT INTO cad_clientes_anexos (Cliente_Id, Nome, Tipo, Url, Observacao, Created_At, Updated_At)
VALUES (@cli_pf2, 'CNH_Carlos.jpg', 'image/jpeg', 'https://storage.oficina.com/docs/CNH_Carlos.jpg', 'CNH digitalizada', NOW(), NULL);

INSERT INTO cad_clientes_anexos (Cliente_Id, Nome, Tipo, Url, Observacao, Created_At, Updated_At)
VALUES (@cli_pf3, 'Orcamento_Luciana.xlsx', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet', 'https://storage.oficina.com/docs/Orcamento_Luciana.xlsx', 'Orçamento aprovado', NOW(), NULL);

INSERT INTO cad_clientes_anexos (Cliente_Id, Nome, Tipo, Url, Observacao, Created_At, Updated_At)
VALUES (@cli_pf4, 'Comprovante_Endereco_Eduardo.pdf', 'application/pdf', 'https://storage.oficina.com/docs/Comprovante_Endereco_Eduardo.pdf', 'Endereço atualizado', NOW(), NULL);

INSERT INTO cad_clientes_anexos (Cliente_Id, Nome, Tipo, Url, Observacao, Created_At, Updated_At)
VALUES (@cli_pf5, 'Contrato_Fernanda.pdf', 'application/pdf', 'https://storage.oficina.com/docs/Contrato_Fernanda.pdf', 'Contrato corporativo', NOW(), NULL);

INSERT INTO cad_mecanicos (Nome, Especialidade, Ativo, Created_At, Updated_At)
VALUES ('Rogério Lima', 'Motor e transmissão', 1, NOW(), NULL);
SET @mec1 = LAST_INSERT_ID();

INSERT INTO cad_mecanicos (Nome, Especialidade, Ativo, Created_At, Updated_At)
VALUES ('Carla Mendes', 'Elétrica automotiva', 1, NOW(), NULL);
SET @mec2 = LAST_INSERT_ID();

INSERT INTO cad_mecanicos (Nome, Especialidade, Ativo, Created_At, Updated_At)
VALUES ('Paulo Ferreira', 'Suspensão e freios', 1, NOW(), NULL);
SET @mec3 = LAST_INSERT_ID();

INSERT INTO cad_mecanicos (Nome, Especialidade, Ativo, Created_At, Updated_At)
VALUES ('Ana Beatriz', 'Diagnóstico eletrônico', 1, NOW(), NULL);
SET @mec4 = LAST_INSERT_ID();

INSERT INTO cad_mecanicos (Nome, Especialidade, Ativo, Created_At, Updated_At)
VALUES ('Fábio Santos', 'Ar-condicionado', 1, NOW(), NULL);
SET @mec5 = LAST_INSERT_ID();

INSERT INTO cad_fornecedores (Razao_Social, Cnpj, Contato, Created_At, Updated_At)
VALUES ('Distribuidora Autosul LTDA', '00987654321098', 'comercial@autosul.com.br', NOW(), NULL);
SET @forn1 = LAST_INSERT_ID();

INSERT INTO cad_fornecedores (Razao_Social, Cnpj, Contato, Created_At, Updated_At)
VALUES ('Peças Premium SA', '00112233445566', 'vendas@pecaspremium.com', NOW(), NULL);
SET @forn2 = LAST_INSERT_ID();

INSERT INTO cad_fornecedores (Razao_Social, Cnpj, Contato, Created_At, Updated_At)
VALUES ('Logparts Comércio', '00334455667788', 'suporte@logparts.com.br', NOW(), NULL);
SET @forn3 = LAST_INSERT_ID();

INSERT INTO cad_fornecedores (Razao_Social, Cnpj, Contato, Created_At, Updated_At)
VALUES ('Motores Brasil', '00778899001122', 'atendimento@motoresbr.com', NOW(), NULL);
SET @forn4 = LAST_INSERT_ID();

INSERT INTO cad_fornecedores (Razao_Social, Cnpj, Contato, Created_At, Updated_At)
VALUES ('AutoClima Serviços', '00556677889900', 'contato@autoclima.com.br', NOW(), NULL);
SET @forn5 = LAST_INSERT_ID();

INSERT INTO cad_veiculos_marcas (Nome, Pais, Created_At, Updated_At)
VALUES ('Toyota', 'Japão', NOW(), NULL);
SET @marca1 = LAST_INSERT_ID();

INSERT INTO cad_veiculos_marcas (Nome, Pais, Created_At, Updated_At)
VALUES ('Chevrolet', 'Estados Unidos', NOW(), NULL);
SET @marca2 = LAST_INSERT_ID();

INSERT INTO cad_veiculos_marcas (Nome, Pais, Created_At, Updated_At)
VALUES ('Volkswagen', 'Alemanha', NOW(), NULL);
SET @marca3 = LAST_INSERT_ID();

INSERT INTO cad_veiculos_marcas (Nome, Pais, Created_At, Updated_At)
VALUES ('Fiat', 'Itália', NOW(), NULL);
SET @marca4 = LAST_INSERT_ID();

INSERT INTO cad_veiculos_marcas (Nome, Pais, Created_At, Updated_At)
VALUES ('Ford', 'Estados Unidos', NOW(), NULL);
SET @marca5 = LAST_INSERT_ID();

INSERT INTO cad_veiculos_modelos (Marca_Id, Nome, Ano_Inicio, Ano_Fim, Created_At, Updated_At)
VALUES (@marca1, 'Corolla XEi', 2019, NULL, NOW(), NULL);
SET @modelo1 = LAST_INSERT_ID();

INSERT INTO cad_veiculos_modelos (Marca_Id, Nome, Ano_Inicio, Ano_Fim, Created_At, Updated_At)
VALUES (@marca2, 'Onix LTZ', 2018, NULL, NOW(), NULL);
SET @modelo2 = LAST_INSERT_ID();

INSERT INTO cad_veiculos_modelos (Marca_Id, Nome, Ano_Inicio, Ano_Fim, Created_At, Updated_At)
VALUES (@marca3, 'T-Cross Highline', 2020, NULL, NOW(), NULL);
SET @modelo3 = LAST_INSERT_ID();

INSERT INTO cad_veiculos_modelos (Marca_Id, Nome, Ano_Inicio, Ano_Fim, Created_At, Updated_At)
VALUES (@marca4, 'Cronos Precision', 2019, NULL, NOW(), NULL);
SET @modelo4 = LAST_INSERT_ID();

INSERT INTO cad_veiculos_modelos (Marca_Id, Nome, Ano_Inicio, Ano_Fim, Created_At, Updated_At)
VALUES (@marca5, 'Ranger XLS', 2017, NULL, NOW(), NULL);
SET @modelo5 = LAST_INSERT_ID();

INSERT INTO cad_veiculos (Cliente_Id, Placa, Marca, Modelo_Id, Ano, Cor, Chassi, Renavam, Combustivel, Observacao, Principal, Created_At, Updated_At)
VALUES (@cli_pf1, 'ABC1D23', 'Toyota', @modelo1, 2021, 'Prata', '9BRBLW12345678901', '01234567890', 'Gasolina', 'Veículo principal', 1, NOW(), NULL);

INSERT INTO cad_veiculos (Cliente_Id, Placa, Marca, Modelo_Id, Ano, Cor, Chassi, Renavam, Combustivel, Observacao, Principal, Created_At, Updated_At)
VALUES (@cli_pf2, 'EFG4H56', 'Chevrolet', @modelo2, 2020, 'Preto', '9BGBCW23456789012', '11223344556', 'Flex', 'Uso diário', 1, NOW(), NULL);

INSERT INTO cad_veiculos (Cliente_Id, Placa, Marca, Modelo_Id, Ano, Cor, Chassi, Renavam, Combustivel, Observacao, Principal, Created_At, Updated_At)
VALUES (@cli_pf3, 'IJK7L89', 'Volkswagen', @modelo3, 2022, 'Branco', '9BWZZZ377VT004251', '22334455667', 'Gasolina', 'Veículo de testes', 1, NOW(), NULL);

INSERT INTO cad_veiculos (Cliente_Id, Placa, Marca, Modelo_Id, Ano, Cor, Chassi, Renavam, Combustivel, Observacao, Principal, Created_At, Updated_At)
VALUES (@cli_pf4, 'MNO0P12', 'Fiat', @modelo4, 2019, 'Vermelho', '8APZZZ15678945012', NULL, 'Flex', 'Uso particular', 1, NOW(), NULL);

INSERT INTO cad_veiculos (Cliente_Id, Placa, Marca, Modelo_Id, Ano, Cor, Chassi, Renavam, Combustivel, Observacao, Principal, Created_At, Updated_At)
VALUES (@cli_pj2, 'QRS3T45', 'Ford', @modelo5, 2021, 'Azul', '9BFZXXE6549873210', '33445566778', 'Diesel', 'Frota TechFleet', 1, NOW(), NULL);

INSERT INTO est_pecas (Codigo, Descricao, Preco_Unitario, Quantidade, Fornecedor_Id, Created_At, Updated_At)
VALUES ('FLT-001', 'Filtro de óleo sintético', 35.90, 120, @forn1, NOW(), NULL);
SET @peca1 = LAST_INSERT_ID();

INSERT INTO est_pecas (Codigo, Descricao, Preco_Unitario, Quantidade, Fornecedor_Id, Created_At, Updated_At)
VALUES ('PST-020', 'Pastilha de freio dianteira', 89.50, 80, @forn2, NOW(), NULL);
SET @peca2 = LAST_INSERT_ID();

INSERT INTO est_pecas (Codigo, Descricao, Preco_Unitario, Quantidade, Fornecedor_Id, Created_At, Updated_At)
VALUES ('VEL-004', 'Vela de ignição iridium', 45.00, 150, @forn3, NOW(), NULL);
SET @peca3 = LAST_INSERT_ID();

INSERT INTO est_pecas (Codigo, Descricao, Preco_Unitario, Quantidade, Fornecedor_Id, Created_At, Updated_At)
VALUES ('ARF-112', 'Filtro de ar cabine', 55.75, 60, @forn4, NOW(), NULL);
SET @peca4 = LAST_INSERT_ID();

INSERT INTO est_pecas (Codigo, Descricao, Preco_Unitario, Quantidade, Fornecedor_Id, Created_At, Updated_At)
VALUES ('ACM-550', 'Compressor de ar condicionado', 980.00, 10, @forn5, NOW(), NULL);
SET @peca5 = LAST_INSERT_ID();

INSERT INTO est_movimentacoes (Peca_Id, Quantidade, Tipo, Referencia, Created_At, Updated_At)
VALUES (@peca1, 50, 'ENTRADA', 'Compra lote março', NOW(), NULL);

INSERT INTO est_movimentacoes (Peca_Id, Quantidade, Tipo, Referencia, Created_At, Updated_At)
VALUES (@peca2, 20, 'SAIDA', 'OS #2024-001', NOW(), NULL);

INSERT INTO est_movimentacoes (Peca_Id, Quantidade, Tipo, Referencia, Created_At, Updated_At)
VALUES (@peca3, 30, 'ENTRADA', 'Reposição estoque padrão', NOW(), NULL);

INSERT INTO est_movimentacoes (Peca_Id, Quantidade, Tipo, Referencia, Created_At, Updated_At)
VALUES (@peca4, 15, 'SAIDA', 'Serviço Luciana Prado', NOW(), NULL);

INSERT INTO est_movimentacoes (Peca_Id, Quantidade, Tipo, Referencia, Created_At, Updated_At)
VALUES (@peca5, 5, 'ENTRADA', 'Compra frota TechFleet', NOW(), NULL);

INSERT INTO os_ordens (Cliente_Id, Mecanico_Id, Descricao_Problema, Status, Data_Abertura, Data_Conclusao, Created_At, Updated_At)
VALUES (@cli_pf1, @mec1, 'Troca de óleo e filtro', 'CONCLUIDA', DATE_SUB(NOW(), INTERVAL 7 DAY), DATE_SUB(NOW(), INTERVAL 6 DAY), NOW(), NULL);
SET @os1 = LAST_INSERT_ID();

INSERT INTO os_ordens (Cliente_Id, Mecanico_Id, Descricao_Problema, Status, Data_Abertura, Data_Conclusao, Created_At, Updated_At)
VALUES (@cli_pf2, @mec2, 'Luz de injeção acesa', 'EM_ANDAMENTO', DATE_SUB(NOW(), INTERVAL 3 DAY), NULL, NOW(), NULL);
SET @os2 = LAST_INSERT_ID();

INSERT INTO os_ordens (Cliente_Id, Mecanico_Id, Descricao_Problema, Status, Data_Abertura, Data_Conclusao, Created_At, Updated_At)
VALUES (@cli_pj1, @mec3, 'Substituição de pastilhas dianteiras', 'CONCLUIDA', DATE_SUB(NOW(), INTERVAL 10 DAY), DATE_SUB(NOW(), INTERVAL 9 DAY), NOW(), NULL);
SET @os3 = LAST_INSERT_ID();

INSERT INTO os_ordens (Cliente_Id, Mecanico_Id, Descricao_Problema, Status, Data_Abertura, Data_Conclusao, Created_At, Updated_At)
VALUES (@cli_pf3, @mec4, 'Diagnóstico ruído motor', 'EM_ABERTO', NOW(), NULL, NOW(), NULL);
SET @os4 = LAST_INSERT_ID();

INSERT INTO os_ordens (Cliente_Id, Mecanico_Id, Descricao_Problema, Status, Data_Abertura, Data_Conclusao, Created_At, Updated_At)
VALUES (@cli_pj2, @mec5, 'Revisão preventiva frota', 'CONCLUIDA', DATE_SUB(NOW(), INTERVAL 15 DAY), DATE_SUB(NOW(), INTERVAL 14 DAY), NOW(), NULL);
SET @os5 = LAST_INSERT_ID();

INSERT INTO os_itens (Ordem_Servico_Id, Peca_Id, Descricao, Quantidade, Valor_Unitario, Created_At, Updated_At)
VALUES (@os1, @peca1, 'Filtro de óleo sintético', 1, 35.90, NOW(), NULL);

INSERT INTO os_itens (Ordem_Servico_Id, Peca_Id, Descricao, Quantidade, Valor_Unitario, Created_At, Updated_At)
VALUES (@os2, @peca3, 'Vela de ignição iridium', 4, 45.00, NOW(), NULL);

INSERT INTO os_itens (Ordem_Servico_Id, Peca_Id, Descricao, Quantidade, Valor_Unitario, Created_At, Updated_At)
VALUES (@os3, @peca2, 'Pastilha de freio dianteira', 1, 89.50, NOW(), NULL);

INSERT INTO os_itens (Ordem_Servico_Id, Peca_Id, Descricao, Quantidade, Valor_Unitario, Created_At, Updated_At)
VALUES (@os4, @peca4, 'Filtro de ar cabine', 1, 55.75, NOW(), NULL);

INSERT INTO os_itens (Ordem_Servico_Id, Peca_Id, Descricao, Quantidade, Valor_Unitario, Created_At, Updated_At)
VALUES (@os5, @peca5, 'Compressor de ar condicionado', 1, 980.00, NOW(), NULL);

INSERT INTO fin_pagamentos (Ordem_Servico_Id, Meio, Valor, Status, Transacao_Id, Created_At, Updated_At)
VALUES (@os1, 'PIX', 249.90, 'PAGO', 'TX-20241013-0001', NOW(), NULL);

INSERT INTO fin_pagamentos (Ordem_Servico_Id, Meio, Valor, Status, Transacao_Id, Created_At, Updated_At)
VALUES (@os2, 'CARTAO', 520.00, 'PROCESSANDO', 'TX-20241015-0002', NOW(), NULL);

INSERT INTO fin_pagamentos (Ordem_Servico_Id, Meio, Valor, Status, Transacao_Id, Created_At, Updated_At)
VALUES (@os3, 'BOLETO', 189.50, 'PAGO', 'TX-20241005-0003', NOW(), NULL);

INSERT INTO fin_pagamentos (Ordem_Servico_Id, Meio, Valor, Status, Transacao_Id, Created_At, Updated_At)
VALUES (@os4, 'PIX', 350.00, 'PENDENTE', NULL, NOW(), NULL);

INSERT INTO fin_pagamentos (Ordem_Servico_Id, Meio, Valor, Status, Transacao_Id, Created_At, Updated_At)
VALUES (@os5, 'BOLETO', 3450.00, 'PAGO', 'TX-20240930-0004', NOW(), NULL);

INSERT INTO fin_nfes (Ordem_Servico_Id, Numero, Chave_Acesso, Status, Created_At, Updated_At)
VALUES (@os1, 'NFE2024-0001', '35241013000000000123550010000000011000000010', 'EMITIDA', NOW(), NULL);

INSERT INTO fin_nfes (Ordem_Servico_Id, Numero, Chave_Acesso, Status, Created_At, Updated_At)
VALUES (@os2, 'NFE2024-0002', '35241015000000000123550020000000022000000020', 'PENDENTE', NOW(), NULL);

INSERT INTO fin_nfes (Ordem_Servico_Id, Numero, Chave_Acesso, Status, Created_At, Updated_At)
VALUES (@os3, 'NFE2024-0003', '35241005000000000123550030000000033000000030', 'EMITIDA', NOW(), NULL);

INSERT INTO fin_nfes (Ordem_Servico_Id, Numero, Chave_Acesso, Status, Created_At, Updated_At)
VALUES (@os4, 'NFE2024-0004', '35241020000000000123550040000000044000000040', 'AGUARDANDO', NOW(), NULL);

INSERT INTO fin_nfes (Ordem_Servico_Id, Numero, Chave_Acesso, Status, Created_At, Updated_At)
VALUES (@os5, 'NFE2024-0005', '35240930000000000123550050000000055000000050', 'EMITIDA', NOW(), NULL);
