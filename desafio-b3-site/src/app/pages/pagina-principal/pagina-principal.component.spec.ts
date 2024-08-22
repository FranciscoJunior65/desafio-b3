import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { NoopAnimationsModule } from '@angular/platform-browser/animations'; // Importa o NoopAnimationsModule
import { of } from 'rxjs';
import { PaginaPrincipalComponent } from './pagina-principal.component';
import { NegocioDataService } from 'src/app/services/negocio-data.service';

describe('PaginaPrincipalComponent', () => {
  let component: PaginaPrincipalComponent;
  let fixture: ComponentFixture<PaginaPrincipalComponent>;
  let mockNegocioDataService: jasmine.SpyObj<NegocioDataService>;

  beforeEach(async () => {
    mockNegocioDataService = jasmine.createSpyObj('NegocioDataService', ['GetCalculoCDB']);
    mockNegocioDataService.GetCalculoCDB.and.returnValue(of({ data: { resultadoBruto: 'R$ 100,00', resultadoLiquido: 'R$ 80,00' } }));

    await TestBed.configureTestingModule({
      imports: [
        FormsModule,
        ReactiveFormsModule,
        MatCardModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        NoopAnimationsModule // Adiciona NoopAnimationsModule aqui
      ],
      declarations: [PaginaPrincipalComponent],
      providers: [
        { provide: NegocioDataService, useValue: mockNegocioDataService }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaginaPrincipalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('deve criar o componente', () => {
    expect(component).toBeTruthy();
  });

  it('deve inicializar o formulário', () => {
    expect(component.form).toBeTruthy();
    expect(component.form.get('valorMonetario')).toBeTruthy();
    expect(component.form.get('prazoMeses')).toBeTruthy();
  });

  it('deve chamar GetCalculoCDB e definir os resultados ao calcular', () => {
    component.form.get('valorMonetario')?.setValue('R$ 100,00');
    component.form.get('prazoMeses')?.setValue('12');
    component.calcular();
    expect(mockNegocioDataService.GetCalculoCDB).toHaveBeenCalled();
    expect(component.resultadoBruto).toBe('R$ 100,00');
    expect(component.resultadoLiquido).toBe('R$ 80,00');
  });

  it('deve limpar os campos e resultados', () => {
    // Configura valores iniciais
    component.form.get('valorMonetario')?.setValue('R$ 100,00');
    component.form.get('prazoMeses')?.setValue('12');
    component.resultadoBruto = 'R$ 100,00';
    component.resultadoLiquido = 'R$ 80,00';

    // Log dos valores antes da limpeza
    console.log('Antes da limpeza:');
    console.log('valorMonetario:', component.form.get('valorMonetario')?.value);
    console.log('prazoMeses:', component.form.get('prazoMeses')?.value);
    console.log('resultadoBruto:', component.resultadoBruto);
    console.log('resultadoLiquido:', component.resultadoLiquido);

    // Chama o método limpar
    component.limpar();

    // Log dos valores após a limpeza
    console.log('Após a limpeza:');
    console.log('valorMonetario:', component.form.get('valorMonetario')?.value);
    console.log('prazoMeses:', component.form.get('prazoMeses')?.value);
    console.log('resultadoBruto:', component.resultadoBruto);
    console.log('resultadoLiquido:', component.resultadoLiquido);

    // Verifica se o valor do formulário e os resultados foram limpos
    expect(component.form.get('valorMonetario')?.value).toBe('');
    expect(component.form.get('prazoMeses')?.value).toBe('');
    expect(component.resultadoBruto).toBe('');
    expect(component.resultadoLiquido).toBe('');
  });

});
