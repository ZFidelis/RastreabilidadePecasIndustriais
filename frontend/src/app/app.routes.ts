import { Routes } from '@angular/router';
import { Home } from './components/home/home';
import { CadastroPeca } from './components/peca/cadastro-peca/cadastro-peca';
import { CadastroEstacao } from './components/estacao/cadastro-estacao/cadastro-estacao';
import { ListaPeca } from './components/peca/lista-peca/lista-peca';
import { ListaEstacao } from './components/estacao/lista-estacao/lista-estacao';
import { ListaMovimentos } from './components/lista-movimentos/lista-movimentos';
import { EditPeca } from './components/peca/edit-peca/edit-peca';
import { EditEstacao } from './components/estacao/edit-estacao/edit-estacao';
import { DeletePeca } from './components/peca/delete-peca/delete-peca';
import { DeleteEstacao } from './components/estacao/delete-estacao/delete-estacao';

export const routes: Routes = [
    {
        path: "home",
        component: Home
    },
    {
      path: "cadastro-peca",
      component: CadastroPeca
    },
    {
      path: "cadastro-estacao",
      component: CadastroEstacao
    },
    {
      path: "lista-peca",
      component: ListaPeca
    },
    {
      path: "lista-estacao",
      component: ListaEstacao
    },
    {
      path: "lista-movimentos",
      component: ListaMovimentos
    },
    {
      path: "editar-peca/:id",
      component: EditPeca
    },
    {
      path: "editar-estacao/:id",
      component: EditEstacao
    },
    {
      path: "deletar-peca",
      component: DeletePeca
    },
    {
      path: "deletar-estacao",
      component: DeleteEstacao
    }
];
