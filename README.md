# 📚 Rastreabilidade de Peças Industriais

## 🧾 Descrição

Aplicação web para gestão do fluxo de peças industriais, com:

✔ Cadastro de partnumbers únicos

✔ Criação de estações e ordens de processamento

✔ Visualização de estações e status das peças (Pendente/Em Processo/Finalizado)

✔ Histórico completo de movimentações

✔ Controle rigoroso do fluxo de produção

Regras de validação:

- Partnumbers devem ser únicos
- Estações não podem ter cadastros duplicados
- Movimentações devem seguir estritamente o fluxo definido (sem pular/retroceder/repetir estações)

---

## 👥 Responsável pelo Projeto
- Isaac Zapoctoczny Fidelis - [Currículo](https://zfidelis.github.io/Curriculo/)

---

## 🛠️ Tecnologias Utilizadas

**Backend**
- .NET 8 (+xUnit para testes)
- ASP.NET Core
- Entity Framework Core
- SQL Server
  
**Frontend**
- Angular 20.1.2

**DevOps**
- Git/GitHub

## 🚀 Como Executar o Projeto

### Pré-requisitos
- .Net SDK 8.0.412
- SQL Server
- Node 20.19.4
- Angular CLI
- Git

### Setup

```bash
# Clone o repositório
git clone https://github.com/ZFidelis/RastreabilidadePecasIndustriais.git
cd RastreabilidadePecasIndustriais

# Backend
cd backend
dotnet restore
dotnet run

# Frontend (em outro terminal)
cd ../frontend
npm install
ng serve
