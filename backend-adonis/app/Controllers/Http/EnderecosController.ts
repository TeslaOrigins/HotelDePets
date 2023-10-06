import type { HttpContextContract } from '@ioc:Adonis/Core/HttpContext'

import Endereco from 'App/Models/Endereco'

export default class EnderecosController {

    public async store({request, response}: HttpContextContract) {

        const body = request.body()
        
        const endereco = await Endereco.create(body)

        response.status(201)
        return {
            data: endereco,
        }
    }

    public async index(){
        const enderecos = await Endereco.all()

        return {
            data: enderecos,
        }
    }

    public async show({params}: HttpContextContract){
        const endereco = await Endereco.findOrFail(params.id)

        return {
            data: endereco,
        }
    }

    public async destroy({params}: HttpContextContract){
        const endereco = await Endereco.findOrFail(params.id)
        
        await endereco.delete()
        return {
            data: endereco,
        }
    }
    public async update({params, request}: HttpContextContract){
        
        const body = request.body()
        const endereco = await Endereco.findOrFail(params.id)

        endereco.logradouro = body.logradouro
        endereco.numero = body.numero
        endereco.cidade = body.cidade
        endereco.estado = body.estado
        
        await endereco.save()
        return {
            data: endereco,
        }
    }
}
